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
        static string root = @"..\..\..\WebSite\";

        internal static void write(string filename, string contents)
        {
            string path = root + filename;
            File.WriteAllText(path, contents);
        }

        internal static bool exists(string filename)
        {
            return File.Exists(root + filename);
        }
    }
}
