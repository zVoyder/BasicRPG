using BasicRPG.Dices;
using BasicRPG.RPGRaces;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicRPG.Character
{
    public enum Statistic
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    class RPGStatistics
    {
        const int pointstoassign = 27, dicerolls = 2;
        Dictionary<Statistic, int> attributes;

        public Dictionary<Statistic, int> Attributes { get => attributes; set => attributes = value; }

        public RPGStatistics(int str = 0, int dex = 0, int con = 0, int @int = 0, int wis = 0, int cha = 0)
        {
            attributes = new Dictionary<Statistic, int>
            {
                { Statistic.Strength, str },
                { Statistic.Dexterity, dex },
                { Statistic.Constitution, con },
                { Statistic.Intelligence, @int },
                { Statistic.Wisdom, wis },
                { Statistic.Charisma, cha }
            };
        }

        public void PointBuy(int min = 8)
        {
            SetAttributes(8, 8, 8, 8, 8, 8);
            attributes = UIHandler.SelectIntOfKeys(attributes, pointstoassign, "Assign your points", min);
        }

        public void RandomGeneration()
        {
            UIHandler.PrintPositionedText("Good Luck Then!\n");

            bool tryagain = true;
            for (int rerolls = dicerolls; tryagain; rerolls--)
            {
                for (int i = 0; i < attributes.Keys.Count; i++)
                {
                    UIHandler.PressAnyKeyToContinue("Press any key to Roll the Dice...");

                    int r = Dice.RollDice(DiceTypes.D6, 4);
                    attributes[attributes.ElementAt(i).Key] = r;

                    UIHandler.PrintPositionedText("You rolled " + r + " for " + attributes.ElementAt(i).Key, TextPosition.Center, ConsoleColor.Red);
                }

                if (rerolls > 1)
                {
                    tryagain = UIHandler.PressSpecificKeyToContinue(ConsoleKey.Enter, "You can still re roll, press [ENTER] to reroll or any key to continue...");
                }
                else
                {
                    tryagain = false;
                    UIHandler.PressAnyKeyToContinue("You can no more re roll, press any key to continue...");
                }

                Console.Clear();
            }
        }

        public void SetAttribute(Statistic atr, int toSet)
        {
            attributes[atr] = toSet;
        }

        public void SetAttributes(int str = 0, int dex = 0, int con = 0, int @int = 0, int wis = 0, int cha = 0)
        {
            SetAttribute(Statistic.Strength, str);
            SetAttribute(Statistic.Dexterity, dex);
            SetAttribute(Statistic.Constitution, con);
            SetAttribute(Statistic.Intelligence, @int);
            SetAttribute(Statistic.Wisdom, wis);
            SetAttribute(Statistic.Charisma, cha);
        }


        public void AddToAttribute(Statistic atr, int toAdd)
        {
            attributes[atr] += toAdd;
        }

        public void AddToAttributes(int str = 0, int dex = 0, int con = 0, int @int = 0, int wis = 0, int cha = 0)
        {
            AddToAttribute(Statistic.Strength, str);
            AddToAttribute(Statistic.Dexterity, dex);
            AddToAttribute(Statistic.Constitution, con);
            AddToAttribute(Statistic.Intelligence, @int);
            AddToAttribute(Statistic.Wisdom, wis);
            AddToAttribute(Statistic.Charisma, cha);
        }

        public void PrintAttributes()
        {
            foreach (KeyValuePair<Statistic, int> kp in attributes)
            {
                UIHandler.PrintPositionedText(kp.Key.ToString().Substring(0, 3).ToUpper() + " = " + kp.Value);
            }

            Console.WriteLine();
        }

        public static RPGStatistics operator +(RPGStatistics a, RPGStatistics b)
        { 
            return new RPGStatistics(
                a.attributes[Statistic.Strength] + b.attributes[Statistic.Strength],
                a.attributes[Statistic.Dexterity] + b.attributes[Statistic.Dexterity],
                a.attributes[Statistic.Constitution] + b.attributes[Statistic.Constitution],
                a.attributes[Statistic.Intelligence] + b.attributes[Statistic.Intelligence],
                a.attributes[Statistic.Wisdom] + b.attributes[Statistic.Wisdom],
                a.attributes[Statistic.Charisma] + b.attributes[Statistic.Charisma]
                );
        }
    }
}
