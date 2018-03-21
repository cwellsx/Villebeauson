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
            pageSections = new PageSections("..\\..\\Pages\\!images.txt", transformLine);
        }

        internal static string getFragment(string id)
        {
            return pageSections.getSection(id);
        }

        static string transformLine(string line)
        {
            if (line.StartsWith("<"))
                return line;
            if (!line.StartsWith("- "))
                return string.Format("<p>{0}</p>", line);
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
