using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace VillebeausonMake
{
    class Page
    {
        internal readonly string title;
        internal readonly string html;
        internal readonly string url;
        internal readonly string filename;
        internal readonly string fragment;
        internal readonly string images;

        internal Page(string title, string id)
        {
            this.title = title;
            this.html = getHtml(id);
            this.url = PageUrl.toUrl(id);
            this.filename = id + ".html";
            this.fragment = Fragments.getFragment(id);
            this.images = Images.getFragment(id);
        }

        static string getHtml(string id)
        {
            string path = "..\\..\\Pages\\" + id + ".txt";
            string[] lines = File.ReadAllLines(path);
            return PageText.getHtml(lines);
        }
    }
}
