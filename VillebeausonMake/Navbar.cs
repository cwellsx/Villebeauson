using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    static class Navbar
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

    static string crlf =
@"
      ";

        internal static string getHtml(Page[] pages, int i, out string lang, out string link)
        {
            int n = pages.Length / 2;
            int remainder = i % 2;

            // ... currently using the last of the three implementations
            // because it gave the best result when page was too narrow for all the navbar links
            int style = 2;

            IEnumerable<string> items = getIndexes(n, remainder).Select(index =>
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

            return string.Format(
                headers[style],
                string.Join(crlf, items),
                getLanguage(pages, i, remainder, out lang, out link)
                );
        }

        static string getLanguage(Page[] pages, int i, int remainder, out string lang, out string link)
        {
            // lang is the language of the current page, the value of the <html lang="{lang}"> attribute
            // language is the language of the other (target) page, the text in the <a class="language"> hyperlink on the navigation bar
            string language;
            string alt;
            int index;
            if (remainder == 0)
            {
                language = "English";
                lang = "fr";
                alt = "en";
                index = i + 1;
            }
            else
            {
                language = "Français";
                lang = "en";
                alt = "fr";
                index = i - 1;
            }
            Page page = pages[index];
            // [Use hreflang for language and regional URLs](https://support.google.com/webmasters/answer/189077?authuser=0)
            link = string.Format(@"<link rel=""alternate"" hreflang=""{0}"" href=""{1}"" />", alt, page.url);
            return string.Format(@"<a class=""btn language border rounded"" href=""{0}"">{1}</a>", page.url, language);
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
