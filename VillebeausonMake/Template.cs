using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace VillebeausonMake
{
    static class Template
    {
        static string templateHtml;

        static Template()
        {
            string path = "..\\..\\template.html";
            templateHtml = File.ReadAllText(path);
            assert(!string.IsNullOrEmpty(templateHtml));
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }

        internal static void output(Page[] pages)
        {
            // assert an even number of pages (pairs of French/English pages)
            int n = pages.Length;
            assert((n % 2) == 0);
            for (int i = 0; i < n; ++i)
            {
                // for each page ...
                Page page = pages[i];
                // ... create the navigation bar
                Navbar navbar = new Navbar(pages, i);
                // ... and output all content
                output(page, navbar);
            }
        }

        static void output(Page page, Navbar navbar)
        {
            // get a copy of the template
            string html = templateHtml;
            // replace content into the template
            html = html.Replace("{lang}", navbar.lang);
            html = html.Replace("{link}", navbar.link);
            html = html.Replace("{title}", page.title);
            html = html.Replace("{navbar}", navbar.navbarHtml);
            string fragment = page.fragment;
            if (fragment!=null)
            {
                fragment = string.Format(
@"<div class=""announce border rounded"">
{0}</div>",
                    fragment
                    );
            }
            html = html.Replace("{announce}", fragment);
            html = html.Replace("{html}", page.html + page.images);

            html = html.Replace("{facebookText}", navbar.facebookText);
            html = html.Replace("{facebookSuffix}", navbar.facebookSuffix);
            // output the result
            Output.write(page.filename, html);
        }
    }
}
