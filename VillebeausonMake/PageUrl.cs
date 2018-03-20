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
    }
}
