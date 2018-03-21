using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    static class PageUrl
    {
        // this flag should be false when building files for the live machine
        // and whenever files are commited to Git
        const bool debug = false;

        internal static string toUrl(string id)
        {
            string suffix = (debug) ? ".html" : null;
            return (id == "index") ? "/" : "/" + id + suffix;
        }

        // search for text like "[events]"
        // and replace it with a corresponding URL like "/events"
        internal static string replace(string text)
        {
            int first = 0;
            for (;;)
            {
                first = text.IndexOf('[', first);
                if (first == -1)
                    break;
                int next = text.IndexOf(']', first);
                assert(next > first);
                assert(text[first - 1] == '"');
                assert(text[next + 1] == '"');
                string found = text.Substring(first + 1, next - first - 1);
                Pages.assertPageId(found);
                text = text.Replace("[" + found + "]", PageUrl.toUrl(found));
            }
            return text;
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }
    }
}
