using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace BasicRPG.Dices
{
    public enum DiceTypes
    {
        D2,
        D3,
        D4,
        D5,
        D6,
        D7,
        D8,
        D9,
        D10,
        D11,
        D12,
        D13,
        D14,
        D15,
        D16,
        D17,
        D18,
        D19,
        D20,
        D40,
        D60,
        D100
    }

    class Dice
    {
        DiceTypes dice;
        public DiceTypes CurrentDice { get => dice; set => dice = value; }
        public int LastRoll => lastRoll;

        int rolls;
        int lastRoll;

        public Dice(DiceTypes dice, int rolls = 1)
        {
            this.dice = dice;
            this.rolls = rolls;
        }

        public int RollDice()
        {
            int total = 0;

            for (int i = 0; i < rolls; i++)
            {
                Random rnd = new Random();
                total += rnd.Next(1, Convert.ToInt32(dice.ToString().Trim('D')) + 1);
            }

            lastRoll = total;
            return total;
        }

        public static int RollDice(DiceTypes dice, int quantity = 1)
        {
            int total = 0;

            for (int i = 0; i < quantity; i++)
            {
                Random rnd = new Random();
                total += rnd.Next(1, Convert.ToInt32(dice.ToString().Trim('D')) + 1);
            }

            return total;
        }

        public override string ToString()
        {
            return dice.ToString();
        }

        public int GetMaxValue()
        {
            return Convert.ToInt32(dice.ToString().Trim('D')) * rolls;
        }

        /*
        const string diceanimationpath = "DiceRollAscii";


        public static int RollDiceWithAnimation(DiceTypes dice, int quantity=1, ConsoleColor color = ConsoleColor.Magenta)
        {
            Console.CursorVisible = false;
            for (int i = 0; i < Directory.GetFiles(diceanimationpath).Length; i++)
            {
                Console.Clear();
                string[] asciidice = File.ReadAllLines(diceanimationpath + @"\" + i + ".txt");
                UIHandler.PrintPositionedText(asciidice, TextPosition.Center, color);
                Thread.Sleep(50);
            }

            Console.CursorVisible = true;
            return RollDice(dice, quantity);
        }*/
    }
}