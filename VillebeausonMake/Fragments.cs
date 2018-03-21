using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    static class Fragments
    {
        static PageSections pageSections;

        static Fragments()
        {
            pageSections = new PageSections("..\\..\\Pages\\!fragments.txt");
        }

        internal static string getFragment(string id)
        {
            return pageSections.getSection(id);
        }
    }
}
