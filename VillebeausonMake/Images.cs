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
            assert(Output.exists(@"img\" + filename));
            return string.Format(@"<p><img src=""/img/{0}""/></p>", filename);
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }
    }
}
