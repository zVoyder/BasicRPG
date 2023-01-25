using BasicRPG.Character;
using BasicRPG.Dices;
using BasicRPG.InventorySystem;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BasicRPG.Encounters
{
    public enum EnemyNames
    {
        crab,
        bee,
        spider,
        gryphon,
        imp,
        knight,
        scorpion,
        wolf
    }

    class Dungeon
    {
        PlayerCharacter pl;
        int floors;
        ChallengeRating cr;
        InventoryShop shop;
        AsciiArt gate, rest;

        public Dungeon(PlayerCharacter player, ChallengeRating startingDifficulty, int numberOfFloors)
        {
            this.cr = startingDifficulty; // Starting point of the dungeon
            pl = player;
            floors = numberOfFloors;
            shop = new InventoryShop();
            gate = new AsciiArt("AsciiArt.Gate");
            rest = new AsciiArt("AsciiArt.Rest");
        }

        public void StartDungeon()
        {
            Console.Clear();

            gate.PrintAsciiArt();
            UIHandler.PressAnyKeyToContinue("YOU OPENED THE DUNGEON'S GATE, IT'S SO DARK IN HERE... PREPARE YOURSELF " + pl.Name + "...", TextPosition.Center, ConsoleColor.Red);

            for(int i = (int)cr; i < floors; i++)
            {
                StartFloor(i);
                RestRoom();
            }
        }

        private void StartFloor(int floor)
        {
            UIHandler.PressAnyKeyToContinue("You are entering the floor " + (floor+1) + " this is gonna be " + (ChallengeRating)floor + " press any key to continue...");


            int numberofenemies = Dice.RollDice(DiceTypes.D10);
            for (int i = 0; i < numberofenemies; i++)
            {
                var rand = new Random();
                var enemynames = Enum.GetNames(typeof(EnemyNames));
                Enemy en = new Enemy(enemynames[rand.Next(enemynames.Length)], (ChallengeRating)((floor > Enum.GetNames(typeof(ChallengeRating)).Length-1) ? Enum.GetNames(typeof(ChallengeRating)).Length - 1 : floor)); // because it could start from a higher CR
                Encounter.Fight(pl, en); // va anche a clearare
            }

            Console.Clear();
            UIHandler.PrintPositionedText("FLOOR NUMBER " + (floor + 1) + " CLEARED!", TextPosition.Center, ConsoleColor.Green);
        }

        private void RestRoom()
        {
            UIHandler.PressAnyKeyToContinue("You have cleared this floor, now you can rest or buy new items and consumables. Press any key to continue...");
            Console.Clear();
            shop.OpenShop(pl);
            Console.Clear();
            pl.Hp.Refresh();
            rest.PrintAsciiArt();
            UIHandler.PrintPositionedText("You have rested. This is your character now");
            pl.PrintCharacter();
            UI.UIHandler.PressAnyKeyToContinue();
        }
    }
}
