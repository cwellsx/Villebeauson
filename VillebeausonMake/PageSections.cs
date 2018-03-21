using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace VillebeausonMake
{
    class PageSections
    {
        Dictionary<string, string> pages = new Dictionary<string, string>();

        internal PageSections(string path) : this(path, s => s)
        {
        }

        internal PageSections(string path, Func<string,string> transformLine)
        {
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
                    {
                        string s = PageUrl.replace(line);
                        s = transformLine(s);
                        fragment.AppendLine("  " + s);
                    }
                }
            }
            pages.Add(id, fragment.ToString());
        }

        internal string getSection(string id)
        {
            string rc = pages[id];
            if (string.IsNullOrEmpty(rc) || rc.All(c => char.IsWhiteSpace(c)))
                return null;
            return rc;
        }
    }
}
