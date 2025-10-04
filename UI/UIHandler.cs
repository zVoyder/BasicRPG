using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Linq;

namespace BasicRPG.UI
{
    public enum TextPosition
    {
        Left,
        Center
    }

    static class UIHandler
    {
        public static Dictionary<T, int> SelectIntOfKeys<T>(Dictionary<T, int> dict, int distribution, string incipit = "Select", int min = 0, int max = 100, TextPosition textpos = TextPosition.Center, ConsoleColor textColor = ConsoleColor.White, ConsoleColor selectedTextColor = ConsoleColor.Yellow, bool printMaxDistr = true)
        {
            int cursorPosition = 0;
            int maxDistr = distribution;
            Dictionary<T, int> tempDict;
            ConsoleKey arrowkey;
            do
            {
                do
                {
                    tempDict = dict;

                    int selectedElement = 0;

                    Console.Clear();

                    Console.WriteLine();

                    PrintPositionedText(incipit, textpos); //Print of the incipit

                    Console.WriteLine();

                    // Printing and coloring of the selected line
                    for (int i = 0; i < tempDict.Keys.Count; i++)
                    {
                        string t;

                        ConsoleColor currentColor;
                        if (cursorPosition == i)
                        {
                            currentColor = selectedTextColor;
                            selectedElement = i;
                            t = "<< " + tempDict.ElementAt(i).Key + " = " + tempDict.ElementAt(i).Value + " >>";
                        }
                        else
                        {
                            currentColor = textColor;
                            t = "" + tempDict.ElementAt(i).Key + " = " + tempDict.ElementAt(i).Value;
                        }

                        PrintPositionedText(t, textpos, currentColor);
                    }

                    Console.WriteLine();
                    if (printMaxDistr)
                        UIHandler.PrintPositionedText("Distribution " + distribution.ToString() + "/" + maxDistr.ToString());
                    else
                        UIHandler.PrintPositionedText("Points " + distribution.ToString());
                    
                    Console.WriteLine();
                    //Pointer
                    arrowkey = Console.ReadKey(true).Key;

                    switch (arrowkey)
                    {

                        case ConsoleKey.UpArrow:
                            cursorPosition--;
                            break;

                        case ConsoleKey.DownArrow:
                            cursorPosition++;
                            break;

                        case ConsoleKey.RightArrow:
                            if (distribution > 0)
                            {
                                tempDict[tempDict.ElementAt(selectedElement).Key]++;
                                distribution--;
                            }
                            break;

                        case ConsoleKey.LeftArrow:
                            if (tempDict[tempDict.ElementAt(selectedElement).Key] > min)
                            {
                                Console.WriteLine(tempDict[tempDict.ElementAt(selectedElement).Key]);
                                tempDict[tempDict.ElementAt(selectedElement).Key]--;
                                distribution++;
                            }
                            break;

                        default:
                            break;
                    }

                    if (tempDict[tempDict.ElementAt(selectedElement).Key] > max)
                    {
                        tempDict[tempDict.ElementAt(selectedElement).Key] = min;
                    }

                    if (cursorPosition < 0)
                    {
                        cursorPosition = tempDict.Keys.Count - 1;
                    }

                    if (cursorPosition > tempDict.Keys.Count - 1)
                    {
                        cursorPosition = 0;
                    }

                } while (arrowkey != ConsoleKey.Enter);

                if (distribution > 0)
                    UIHandler.PressAnyKeyToContinue("You did not spent all the points, please use all your points, press any key to continue...");

            } while (distribution > 0);

            return tempDict;
        }

        /// <summary>
        /// Allow the user to select a string from a string array and return the index of the selected string
        /// </summary>
        /// <param name="incipit">The initial message</param>
        /// <param name="choices">The string array</param>
        /// <param name="textpos">Position of the text</param>
        /// <param name="textColor">Base color of the text</param>
        /// <param name="selectedTextColor">Color of the selected line</param>
        /// <returns>the int index of the selected string</returns>
        public static int SelectiveChoice(string incipit, string[] choices, TextPosition textpos = TextPosition.Center, ConsoleColor textColor = ConsoleColor.White, ConsoleColor selectedTextColor = ConsoleColor.Yellow, ConsoleColor incipitColor = ConsoleColor.White)
        {
            int cursorPosition = 0;
            ConsoleKey arrkey;
            do
            {
                Console.Clear();

                Console.WriteLine("\n");

                PrintPositionedText(incipit.Split("\n"), textpos, incipitColor); //Print of the incipit

                Console.WriteLine("\n");

                // Printing and coloring of the selected line
                for (int i = 0; i < choices.Length; i++)
                {
                    string t;

                    ConsoleColor currentColor;
                    if (cursorPosition == i)
                    {
                        currentColor = selectedTextColor;
                        t = ">> " + choices[i] + " <<";
                    }
                    else
                    {
                        currentColor = textColor;
                        t = "" + choices[i];
                    }

                    PrintPositionedText(t, textpos, currentColor);
                }

                //Pointer
                arrkey = Console.ReadKey(true).Key;
                if (arrkey == ConsoleKey.UpArrow)
                {
                    cursorPosition--;
                }
                else if (arrkey == ConsoleKey.DownArrow)
                {
                    cursorPosition++;
                }

                if (cursorPosition < 0)
                {
                    cursorPosition = choices.Length - 1;
                }

                if (cursorPosition > choices.Length - 1)
                {
                    cursorPosition = 0;
                }

                //new Thread(() => Console.Beep()).Start();
            } while (arrkey != ConsoleKey.Enter);


            return cursorPosition;
        }

        /// <summary>
        /// Allow the user to select a string from a string array and return the index of the selected string
        /// </summary>
        /// <param name="art">The ascii art i want to print before the choice lines</param>
        /// <param name="incipit">The initial message</param>
        /// <param name="choices">The string array</param>
        /// <param name="textpos">Position of the text</param>
        /// <param name="textColor">Base color of the text</param>
        /// <param name="selectedTextColor">Color of the selected line</param>
        /// <returns>the int index of the selected string</returns>
        public static int SelectiveChoice(AsciiArt art, string incipit, string[] choices, TextPosition textpos = TextPosition.Center, ConsoleColor textColor = ConsoleColor.White, ConsoleColor selectedTextColor = ConsoleColor.Yellow)
        {
            string[] combined = art.ArtStringLines.Union(incipit.Split("\n")).ToArray();

            string combinedString = string.Join("\n", combined);

            Console.WriteLine(combinedString);

            return SelectiveChoice(combinedString, choices, textpos, textColor, selectedTextColor);
        }

        public static void PrintPositionedText(string text, TextPosition pos = TextPosition.Center, ConsoleColor textColor = ConsoleColor.White)
        {
            ConsoleColor currentColor = Console.ForegroundColor;

            try
            {
                switch (pos)
                {
                    case TextPosition.Center:
                        Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                        break;
                    case TextPosition.Left:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(pos), pos, null);
                }
            }
            catch (Exception)
            {
                // Menu.ShowWindow(Menu.ThisConsole, Menu.MAXIMIZE);
            }

            Console.ForegroundColor = textColor;
            Console.WriteLine(text);
            Console.ForegroundColor = currentColor;
        }

        public static void PrintPositionedText(string[] texts, TextPosition pos = TextPosition.Center, ConsoleColor textColor = ConsoleColor.White)
        {
            foreach (string t in texts)
            {
                ConsoleColor currentColor = Console.ForegroundColor;

                switch (pos)
                {
                    case TextPosition.Center:
                        Console.SetCursorPosition((Console.WindowWidth - t.Length) / 2, Console.CursorTop);
                        break;
                }

                Console.ForegroundColor = textColor;
                Console.WriteLine(t);
                Console.ForegroundColor = currentColor;
            }
        }

        public static void PressAnyKeyToContinue(string msg = "Press any key to continue...", TextPosition textpos = TextPosition.Center, ConsoleColor textColor = ConsoleColor.White)
        {
            UIHandler.PrintPositionedText(msg, textpos, textColor);
            Console.ReadKey(true);
        }

        public static bool PressSpecificKeyToContinue(ConsoleKey key, string msg = "Press...", TextPosition textPos = TextPosition.Center)
        {
            UIHandler.PrintPositionedText(msg, textPos);

            if (Console.ReadKey(true).Key == key)
            {
                return true;
            }

            return false;
        }

        public static int SliderSelector(int min, int max, string msg = "", ConsoleColor color = ConsoleColor.White)
        {
            min = Math.Min(min, max);
            max = Math.Max(min, max);

            int i = min;

            ConsoleKey input;

            do
            {
                Console.Clear();

                PrintPositionedText(msg);
                PrintPositionedText("<< " + i + " >>", TextPosition.Center, color);

                input = Console.ReadKey(true).Key;

                i += input switch
                {
                    ConsoleKey.RightArrow => 1,
                    ConsoleKey.LeftArrow => -1,
                    ConsoleKey.UpArrow => 10,
                    ConsoleKey.DownArrow => -10,
                    _ => 0
                };


                if (i < min)
                {
                    i = max;
                }

                if (i > max)
                {
                    i = min;
                }

            } while (input != ConsoleKey.Enter);

            return i;
        }

        public static string AskLine(string inputmsg = "> ")
        {
            Console.Write(inputmsg);
            return Console.ReadLine();
        }
    }
}
