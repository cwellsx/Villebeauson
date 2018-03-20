using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillebeausonMake
{
    static class PageText
    {
        internal static string getHtml(string[] lines, bool fancy)
        {
            return (fancy) ? getFancy(lines) : getSimple(lines);
        }

        enum State
        {
            Start,
            Text,
            Break
        }

        static string getFancy(string[] lines)
        {
            StringBuilder html = new StringBuilder();

            State state = State.Start;
            foreach (string line in lines)
            {
                bool isHeading = line.StartsWith("# ");
                bool isBreak = string.IsNullOrEmpty(line);

                string heading = (isHeading) ? string.Format("<h1>{0}</h1>", line.Substring(2)) : null;

                switch (state)
                {
                    case State.Start:
                        assert(isHeading);
                        html.AppendLine(heading);
                        html.AppendLine("<p>");
                        state = State.Text;
                        break;
                    case State.Text:
                        assert(!isHeading);
                        if (isBreak)
                        {
                            html.AppendLine("</p>");
                            state = State.Break;
                        }
                        else
                        {
                            html.AppendLine("  " + line);
                        }
                        break;
                    case State.Break:
                        assert(!isBreak);
                        if (isHeading)
                        {
                            html.AppendLine(heading);
                            html.AppendLine("<p>");
                        }
                        else
                        {
                            html.AppendLine("<p>");
                            html.AppendLine("  " + line);
                        }
                        state = State.Text;
                        break;
                }
            }

            assert(state == State.Text);
            html.Append("</p>");
            return html.ToString();
        }

        static string getSimple(string[] lines)
        {
            StringBuilder html = new StringBuilder();
            foreach (string line in lines)
            {
                bool isHeading = line.StartsWith("# ");
                bool isBreak = string.IsNullOrEmpty(line);

                if (isBreak)
                    continue;

                if (isHeading)
                {
                    html.AppendLine(string.Format("<h2>{0}</h2>", line.Substring(2)));
                }
                else
                {
                    html.AppendLine(string.Format("<p>{0}</p>", line));
                }
            }
            return html.ToString();
        }

        static void assert(bool b)
        {
            if (!b)
                throw new Exception();
        }
    }
}
