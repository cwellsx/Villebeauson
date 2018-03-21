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
                string lang;
                string navbar = Navbar.getHtml(pages, i, out lang);
                // ... and output all content
                output(page, navbar, lang);
            }
        }

        static void output(Page page, string navbar, string lang)
        {
            // get a copy of the template
            string html = templateHtml;
            // replace content into the template
            html = html.Replace("{lang}", lang);
            html = html.Replace("{title}", page.title);
            html = html.Replace("{navbar}", navbar);
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
            html = html.Replace("{html}", page.html);
            // output the result
            Output.write(page.filename, html);
        }
    }
}
