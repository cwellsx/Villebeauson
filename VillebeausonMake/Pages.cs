using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace VillebeausonMake
{
    /*
     * Filenames:
     * 
     * - index
     * - repetitions
     * - evenements
     * - musique
     * 
     * - welcome
     * - rehearsals
     * - events
     * - music
     */

    static class Pages
    {
        internal static Page[] getPages()
        {
            return pageIds.Select(pageId => new Page(pageId.title, pageId.id)).ToArray();
        }

        struct PageId
        {
            internal string title;
            internal string id;
            internal PageId(string title, string id)
            {
                this.title = title;
                this.id = id;
            }
        }

        static PageId[] pageIds;

        static Pages()
        {
            string path = "..\\..\\Pages\\!pages.txt";
            string[] lines = File.ReadAllLines(path);

            assert((lines.Length % 2) == 0);

            pageIds = lines.Select(line => getPageId(line)).ToArray();
        }

        static PageId getPageId(string line)
        {
            string[] split = line.Split('\t');
            assert(split.Length == 2);
            string title = split[0];
            string id = split[1];
            assert(title.All(c => char.IsLetter(c)));
            // [Char.IsLetter() and Ascii](https://blogs.msdn.microsoft.com/brada/2004/03/08/char-isletter-and-ascii/)
            assert(id.All(c => char.IsLetter(c) && c <= 0x007a));
            return new PageId(title, id);
        }

        internal static void assertPageId(string id)
        {
            assert(pageIds.Any(found => found.id == id));
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }
    }
}
