using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    class Program
    {
        static void Main(string[] args)
        {
            // read all pages (and fragments)
            Page[] pages = Pages.getPages();
            // push page contents into the HTML template for output
            Template.output(pages);
        }
    }
}
