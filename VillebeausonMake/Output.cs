using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace VillebeausonMake
{
    static class Output
    {
        internal static void write(string filename, string contents)
        {
            string path = @"..\\..\\..\\WebSite\\" + filename;
            File.WriteAllText(path, contents);
        }
    }
}
