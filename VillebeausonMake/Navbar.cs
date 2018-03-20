using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    static class Navbar
    {
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

@"  <header>
    {1}
    <div>
      {0}
    </div>
  </header>"
        };

        static string[] anchors =
        {
            @"<li class=""nav-item""><a class=""nav-link{0}"" href=""{1}"">{2}</a></li>",
            @"<a class=""nav-item nav-link{0}"" href=""{1}"">{2}</a>",
            @"<a class=""x{0}"" href=""{1}"">{2}</a>"
        };

    static string crlf =
@"
      ";

        internal static string getHtml(Page[] pages, int i)
        {
            int n = pages.Length / 2;
            int remainder = i % 2;

            int style = 2;

            IEnumerable<string> items = getIndexes(n, remainder).Select(index =>
            {
                bool isActive = (index == i);
                Page page = pages[index];
                return string.Format(
                    //@"<li class=""nav-item""><a class=""nav-link{0}"" href=""{1}"">{2}</a></li>",
                    //@"<a class=""nav-item nav-link{0}"" href=""{1}"">{2}</a>",
                    anchors[style],
                    (isActive) ? " active" : null,
                    page.url,
                    page.title
                    );
            });

            return string.Format(
                headers[style],
                string.Join(crlf, items),
                getLanguage(pages, i, remainder)
                );
        }

        static string getLanguage(Page[] pages, int i, int remainder)
        {
            string language;
            int index;
            if (remainder == 0)
            {
                language = "English";
                index = i + 1;
            }
            else
            {
                language = "Français";
                index = i - 1;
            }
            Page page = pages[index];
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
