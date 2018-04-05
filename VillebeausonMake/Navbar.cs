using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    class Navbar
    {
        // I experimented with three implementations
        static string[] headers =
        {
@"<header class=""navbar navbar-expand"">
  <ul class=""navbar-nav"">
    {0}
  </ul>
  {1}
</header>",

@"<header class=""navbar navbar-expand"">
  <div class=""navbar-nav"">
    {0}
  </div>
  {1}
</header>",

@"  <nav>
    {1}
    <div>
      {0}
    </div>
  </nav>"
        };

        // ... three implementations
        static string[] anchors =
        {
            @"<li class=""nav-item""><a class=""nav-link{0}"" href=""{1}"">{2}</a></li>",
            @"<a class=""nav-item nav-link{0}"" href=""{1}"">{2}</a>",
            @"<a class=""x{0}"" href=""{1}"">{2}</a>"
        };

        // ... currently using the last of the three implementations
        // because it gave the best result when page was too narrow for all the navbar links
        const int style = 2;

        static string crlf =
@"
      ";

        internal readonly string navbarHtml;
        // lang is the language of the current page, the value of the <html lang="{lang}"> attribute
        internal readonly string lang;
        internal readonly string link;

        internal readonly string facebookText;
        internal readonly string facebookSuffix;

        internal Navbar(Page[] pages, int i)
        {
            int n = pages.Length / 2;
            int remainder = i % 2;

            IEnumerable<string> items = getItems(pages, i, n, remainder);

            // languageLabel is the language of the other (target) page, the text in the <a class="language"> hyperlink on the navigation bar
            string languageLabel;
            string alt;
            int index;
            if (remainder == 0)
            {
                languageLabel = "English";
                this.lang = "fr";
                alt = "en";

                facebookText = "Retrouvez-nous sur Facebook";
                facebookSuffix = "_fr_FR";

                index = i + 1;
            }
            else
            {
                languageLabel = "Français";
                this.lang = "en";
                alt = "fr";

                facebookText = "Find us on Facebook";
                facebookSuffix = "";

                index = i - 1;
            }
            Page page = pages[index];
            // [Use hreflang for language and regional URLs](https://support.google.com/webmasters/answer/189077?authuser=0)
            this.link = string.Format(@"<link rel=""alternate"" hreflang=""{0}"" href=""{1}"" />", alt, page.url);
            string languageButton =  string.Format(@"<a class=""btn language border rounded"" href=""{0}"">{1}</a>", page.url, languageLabel);

            this.navbarHtml = string.Format(
                headers[style],
                string.Join(crlf, items),
                languageButton
                );
        }

        static IEnumerable<string> getItems(Page[] pages, int i, int n, int remainder)
        {
            return getIndexes(n, remainder).Select(index =>
            {
                bool isActive = (index == i);
                Page page = pages[index];
                return string.Format(
                    anchors[style],
                    (isActive) ? " active" : null,
                    page.url,
                    page.title
                    );
            });
        }

        static IEnumerable<int> getIndexes(int n, int remainder)
        {
            for (int i = 0; i < n; ++i)
            {
                yield return (i * 2) + remainder;
            }
        }
    }
}
