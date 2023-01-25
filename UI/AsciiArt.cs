using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace BasicRPG.UI
{
    class AsciiArt
    {
        string[] art;
        public string[] ArtStringLines { get => art; set => art = value; }


        public AsciiArt(string[] art)
        {
            this.art = art;
        }

        public AsciiArt(string resourcename)
        {
            string[] lines = Properties.Resources.ResourceManager.GetString(resourcename).Split("\n");
            Array.Resize(ref lines, lines.Length + 1);
            lines[lines.Length-1] = ""; // I add an endline so i can print other info on the bottom if i want

            art = lines;
        }

        /// <summary>
        /// Modify the endline of a new asciiart with the new endline
        /// </summary>
        /// <param name="str"></param>
        /// <returns>the new asciiart with the modified endline</returns>
        public static AsciiArt ModifyEndline(AsciiArt art, string str)
        {
            string[] newlines = new string[art.ArtStringLines.Length];

            Array.Copy(art.ArtStringLines, newlines, newlines.Length);

            string spaces = new string(' ', GetStringWithMaxLength(art.ArtStringLines).Length / 2 - str.Length / 2);
            string rem = new string(' ', GetStringWithMaxLength(art.ArtStringLines).Length - spaces.Length - str.Length - 1); // I have to do it like this becuase the length could be an odd number.
            newlines[newlines.Length - 1] = spaces + str + rem;
            return new AsciiArt(newlines);
        }

        public void PrintAsciiArt(TextPosition artposition = TextPosition.Center, ConsoleColor textColor = ConsoleColor.White)
        {
            UIHandler.PrintPositionedText(art, artposition, textColor);
        }

        public static AsciiArt operator +(AsciiArt a, AsciiArt b)
        {
            int maxlines = Math.Max(a.ArtStringLines.Length, b.ArtStringLines.Length);

            string[] artcombinedlines = new string[maxlines];

            int awidth = GetStringWithMaxLength(a.ArtStringLines).Length-1;
            int bwidth = GetStringWithMaxLength(b.ArtStringLines).Length-1;

            for (int i = 0; i < maxlines; i++)
            {
                string ast, bst;

                try
                {
                    if (a.ArtStringLines[i] != "\r")
                    {
                        ast = a.ArtStringLines[i].Replace("\r", "");
                    }
                    else
                    {
                        ast = new string(' ', awidth);
                    }
                }
                catch (Exception)
                {
                    ast = new string(' ', awidth);
                }

                try
                {
                    if (b.ArtStringLines[i] != "\r")
                    {
                        bst = b.ArtStringLines[i].Replace("\r", "");
                    }
                    else
                    {
                        bst = new string(' ', bwidth);
                    }
                }
                catch (Exception)
                {
                    bst = new string(' ', bwidth);
                }

                artcombinedlines[i] = ast + bst;
            }

            return new AsciiArt(artcombinedlines);
        }


        private static string GetStringWithMaxLength(string[] strings)
        {
            List<string> strs = strings.OfType<string>().ToList();

            return strs.OrderByDescending(s => s.Length).First();
        }
    }
}
