using System;
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
            Console.Title = "Ekylios";
            Console.CursorVisible = false;

            Menu menu = new Menu();
            PlayerCharacter player = new PlayerCharacter();
            MusicSheet menuSheet = new MusicSheet("Music.Ekylios", BpmTempo.Larghetto, .5f);
            menuSheet.Loop();

            int m = menu.StartMenu();

            if (m == menu.ExitCode())
                Environment.Exit(0);
            Console.Clear();

            if (m == 0)
            {
                player.CharacterCreation();
                menuSheet.Pause();
                Dungeon dungeon = new Dungeon(player, ChallengeRating.Easy, 10);
                dungeon.StartDungeon();
            }
            // else
            // {
            //     UIHandler.PressAnyKeyToContinue("Not Available in Pre-Alpha");
            //     menumusic.Pause();
            //     Main();
            // }
        }
    }
}

/*
    //Example of Input/Output storytelling system
    Dictionary<string, string> test = new Dictionary<string, string>();
    test.Add("guarda vedi osserva", "incredibile");
    Console.WriteLine(test.ElementAt(0).Key.Contains("vedi"));
*/