using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    class Program
    {
        const bool debug = false;

        static void Main(string[] args)
        {
            Page[] pages = Pages.getPages();
            Template.output(pages);
        }

        internal static string toUrl(string id)
        {
            string suffix = (Program.debug) ? ".html" : null;
            return (id == "index") ? "/" : "/" + id + suffix;
        }
    }
}
