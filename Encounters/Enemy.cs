using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BasicRPG.Encounters
{
    public enum ChallengeRating
    {
        Easy,
        Normal,
        Difficult,
        Hard,
        Extreme,
        Insane,
        Calamity
    }

    class Enemy
    {
        static Dictionary<ChallengeRating, Dice> fullcr = new Dictionary<ChallengeRating, Dice>()
        {
            {
                ChallengeRating.Easy, new Dice(DiceTypes.D4, (int)Math.Sqrt(5))
            },
            {
                ChallengeRating.Normal, new Dice(DiceTypes.D6, (int)Math.Sqrt(12))
            },
            {
                ChallengeRating.Difficult, new Dice(DiceTypes.D8, (int)Math.Pow(2, 2))
            },
            {
                ChallengeRating.Hard, new Dice(DiceTypes.D10, (int)Math.Pow(2.5, 2))
            },
            {
                ChallengeRating.Extreme, new Dice(DiceTypes.D12, (int)Math.Pow(3, 2))
            },
            {
                ChallengeRating.Insane, new Dice(DiceTypes.D20, (int)Math.Pow(3.5, 2))
            },
            {
                ChallengeRating.Calamity, new Dice(DiceTypes.D20, (int)Math.Pow(5, 2))
            }
        };
        
        static readonly string[] DeathPhrases =
        {
            "is dead",
            "died",
            "fainted",
            "explodes in 100 pieces",
            "takes its last breath and dies",
            "paints the floor with its blood",
            "is shredded",
            "sees the light",
            "covers the sorrounding with its flesh"
        };

        ChallengeRating cr;
        AsciiArt art;
        string name;

        Currency goldReward;
        int expReward;

        HealthPoints hp;

        public Dictionary<string, Dice> attacks;

        public AsciiArt Art { get => art; }
        public string Name { get => name; }
        internal HealthPoints Hp { get => hp; set => hp = value; }
        public ChallengeRating Cr { get => cr; }

        public Enemy(string resourcename, ChallengeRating cr)
        {
            this.cr = cr;

            expReward = 100 * Dice.RollDice(fullcr[cr].CurrentDice, 1);
            goldReward = new Currency(0, 0, fullcr[cr].RollDice() * 100);
            hp = new HealthPoints(fullcr[cr].RollDice(), '☠');

            string[] lines = Properties.Resources.ResourceManager.GetString(resourcename).Split("[PARAM]");
            attacks = new Dictionary<string, Dice>();

            this.name = lines[0];

            string[] fullatts = lines[1].Split(",");

            foreach (string att in fullatts) //Converting the strings in a dictionary
            {
                string[] descanddice = att.Replace("\r\n", "").Split("_"); //String cleaning
                attacks.Add(descanddice[0], new Dice(Enum.Parse<DiceTypes>(descanddice[1])));
            }

            this.art = new AsciiArt(lines[2].Split("\n"));
        }

        public int Attack()
        {
            string att = attacks.ElementAt(new Random().Next(0, attacks.Count)).Key;
            int nr = attacks[att].RollDice() + CalculateAttackBonus();
            UIHandler.PrintPositionedText(name + " attacks with " + att + " and does " + nr + " DMG!");
            return nr;
        }

        public bool TakeDamage(int damagepoints)
        {
            UIHandler.PrintPositionedText(name + " takes " + damagepoints + " damage", TextPosition.Center, ConsoleColor.DarkYellow);
            return !hp.Damage(damagepoints);
        }

        public void Death()
        {
            UIHandler.PrintPositionedText(name + " " + DeathPhrases[new Random().Next(0, DeathPhrases.Length)], TextPosition.Center, ConsoleColor.DarkRed);
        }

        public Currency GetReward(out int exp)
        {
            exp = expReward;
            return goldReward;
        }

        private int CalculateAttackBonus()
        {
            int crValue = (int)cr + 1;
            int bonus = 2 + (int)Math.Pow(crValue * 7, 0.8);
            return bonus;
        }
    }
}