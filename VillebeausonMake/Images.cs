using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    class Images
    {
        static PageSections pageSections;

        static Images()
        {
            // use PageSections to handle the same text file format as Fragments
            // except transformLine because !images.txt has a more complicated line format.
            pageSections = new PageSections("..\\..\\Pages\\!images.txt", transformLine);
        }

        internal static string getFragment(string id)
        {
            return pageSections.getSection(id);
        }

        static string transformLine(string line)
        {
            if (line.StartsWith("<"))
                // it's HTML
                return line;
            if (!line.StartsWith("- "))
                // it's plain text
                return string.Format("<p>{0}</p>", line);
            // else it's a hyphen followed by an image filename
            string filename = line.Substring(2);
            string alt = null;
            if (filename.Contains('|'))
            {
                string[] split = filename.Split('|');
                assert(split.Length == 2);
                filename = split[0];
                alt = split[1];
                assert(Output.exists(@"img\" + alt));
            }
            assert(Output.exists(@"img\" + filename));
            return (alt == null)
                ? string.Format(@"<p><img src=""/img/{0}""/></p>", filename)
                : string.Format(@"<p><a href=""/img/{1}"" target=""_blank""><img src=""/img/{0}""/></a></p>", filename, alt);
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }
    }
}
