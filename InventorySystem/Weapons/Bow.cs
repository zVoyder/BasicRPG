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

        readonly string[] attackDescriptions = {
            "You nock your arrow and shoot at the target.\n",
            "You defly draw an arrow from your quiver and shoot at the target.\n",
            "You nock your arrow and with precision you fire at the enemy.\n",
            "You take an arrow from your quiver and quickly shoot at the enemy.\n",
            "You feel the air blowing through your hands as you nock your arrow directed at the enemy. With decision you shoot the arrow.\n",
            "You decisevely take your arrow and shoot at the target.\n"
        };

        public Bow(string name, string desc, Currency value, double weight, Dice damageDice, Statistic attraffinity, int arrows) 
            : base(name, desc, value, weight, damageDice, attraffinity)
        {
            Arrows = arrows;
            attacks = attackDescriptions;
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

            return -1;
        }

        public override string ToString()
        {
            return base.ToString() + "; Arrows Left: " + Arrows;
        }

        public override string ToShortString()
        {
            return base.ToShortString() + " ##---> " + Arrows;
        }
    }
}
