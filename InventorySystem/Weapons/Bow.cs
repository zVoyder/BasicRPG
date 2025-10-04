using System;
using System.Collections.Generic;
using System.Text;
using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.Character.RPGClasses;
using BasicRPG.UI;
using BasicRPG.Character;

namespace BasicRPG.InventorySystem.Weapons
{
    class Bow : InventorySystem.Weapons.Weapon
    {
        public int Arrows { get ; set ; }
        public int MaxArrows { get; set; }

        readonly string[] attackDescriptions = {
            "You nock your arrow and shoot at the target.\n",
            "You defly draw an arrow from your quiver and shoot at the target.\n",
            "You nock your arrow and with precision you fire at the enemy.\n",
            "You take an arrow from your quiver and quickly shoot at the enemy.\n",
            "You feel the air blowing through your hands as you nock your arrow directed at the enemy. With decision you shoot the arrow.\n",
            "You decisevely take your arrow and shoot at the target.\n"
        };

        public Bow(string name, string desc, Currency value, double weight, Dice damageDice, Statistic attraffinity, int arrows, int maxarrows)
            : base(name, desc, value, weight, damageDice, attraffinity)
        {
            Arrows = arrows;
            attacks = attackDescriptions;
            MaxArrows = maxarrows;
        }
        
        public int RefillArrows(int n)
        {
            if (n < 0)
                throw new Exception("Error: Negative numbers");

            int arrowsBefore = Arrows;
            Arrows += n;
            Arrows = Math.Clamp(Arrows, 0, MaxArrows);

            if (Arrows > MaxArrows)
                Arrows = MaxArrows;

            return Arrows - arrowsBefore;
        }

        public override int Use()
        {
            if (Arrows > 0)
            {
                UIHandler.PrintPositionedText("Arrows Lefts: " + --Arrows);
                return base.Use();
            }
            else
                Console.WriteLine("You have run out of arrows.");

            return 0;
        }

        public override string ToString()
        {
            return base.ToString() + "; Arrows Left: " + Arrows + "/" + MaxArrows;
        }

        public override string ToShortString()
        {
            return base.ToShortString() + " ##---> " + Arrows + "/" + MaxArrows;
        }
    }
}
