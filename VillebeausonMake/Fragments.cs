using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace VillebeausonMake
{
    static class Fragments
    {
        static Dictionary<string, string> pages = new Dictionary<string, string>();

        static Fragments()
        {
            string path = "..\\..\\Pages\\!fragments.txt";
            string[] lines = File.ReadAllLines(path);

            StringBuilder fragment = null;
            string id = null;
            foreach (string line in lines)
            {
                if (line.StartsWith("# "))
                {
                    // new page
                    if (id != null)
                        pages.Add(id, fragment.ToString());
                    id = line.Substring(2);
                    fragment = new StringBuilder();
                }
                else
                {
                    if (!string.IsNullOrEmpty(line))
                        fragment.AppendLine("  " + line);
                }
            }
            pages.Add(id, fragment.ToString());
        }

        internal static string getFragment(string id)
        {
            string rc = pages[id];
            if (string.IsNullOrEmpty(rc) || rc.All(c => char.IsWhiteSpace(c)))
                return null;
            rc = PageUrl.replace(rc);
            return rc;
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }
    }
}
