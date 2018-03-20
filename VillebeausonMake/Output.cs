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

        //internal static void write(Page[] pages)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("Options +FollowSymLinks");
        //    sb.AppendLine();
        //    sb.AppendLine("RewriteEngine On");
        //    sb.AppendLine();
        //    sb.AppendLine("RewriteRule ^/foo$ /hello.txt");
        //    sb.AppendLine("RewriteRule foo /hello.txt");
        //    sb.AppendLine();
        //    foreach(Page page in pages.Skip(1))
        //    {
        //        sb.AppendLine(string.Format("RewriteRule {0} /{0}.html", page.url.Substring(1)));
        //    }

        //    write(".htaccess", sb.ToString());
        //}
    }
}
