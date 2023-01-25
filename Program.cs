using System;
using System.Collections.Generic;
using System.Text;

using BasicRPG.UI;
using BasicRPG.Character;
using BasicRPG.Music;
using BasicRPG.Encounters;

namespace BasicRPG
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Menu.ShowWindow(Menu.ThisConsole, Menu.MAXIMIZE);
            Console.Title = "Ekylios";

            Menu menu = new Menu(Console.LargestWindowWidth, Console.LargestWindowHeight);
            PlayerCharacter pl = new PlayerCharacter();
            MusicSheet menumusic = new MusicSheet("Music.JingleBells", BpmTempo.Lento, .5f);

            menumusic.Loop();

            int m = menu.StartMenu();

            if (m == menu.ExitCode())
            {
                Environment.Exit(0);
            }

            Console.Clear();

            if (m == 1)
            {
                pl.CharacterCreation();
                menumusic.Pause();

                Dungeon dungeon = new Dungeon(pl, ChallengeRating.Easy, 10);

                dungeon.StartDungeon();
            }
            else
            {
                UIHandler.PressAnyKeyToContinue("Not Available in Pre-Alpha");
                menumusic.Pause();
                Main();
            }
        }
    }
}


/*
            //Esempio di come farò gli input/output della storyline
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("guarda vedi osserva", "incredibile");
            Console.WriteLine(test.ElementAt(0).Key.Contains("vedia"));
*/