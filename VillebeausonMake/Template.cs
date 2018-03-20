using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace VillebeausonMake
{
    static class Template
    {
        static string templateHtml;

        static Template()
        {
            string path = "..\\..\\template.html";
            templateHtml = File.ReadAllText(path);
            assert(!string.IsNullOrEmpty(templateHtml));
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }

        internal static void output(Page[] pages)
        {
            int n = pages.Length;
            assert((n % 2) == 0);
            for (int i = 0; i < n; ++i)
            {
                Page page = pages[i];
                string navbar = Navbar.getHtml(pages, i);
                output(page, navbar);
            }
        }

        static void output(Page page, string navbar)
        {
            string html = templateHtml;
            html = html.Replace("{title}", page.title);
            html = html.Replace("{navbar}", navbar);
            html = html.Replace("{html}", page.html);

            Output.write(page.filename, html);
        }
    }
}
