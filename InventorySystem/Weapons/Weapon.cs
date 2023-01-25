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
    class Weapon : Item
    {
        protected string[] attacks;
        
        Statistic attributeAffinity; // The attribute affinity of the weapon, i leave it here due to specific cases where a sword can have an Arcana affinity instead of Athletic.

        Dice damageDice;

        public Statistic AttributeAffinity { get => attributeAffinity; set => attributeAffinity = value; }

        public Weapon(string name, string desc, Currency value, double weight, Dice damageDice, Statistic attraffinity) 
        : base(name, desc, value, weight)
        {
            this.damageDice = damageDice;
            attacks = new string[] { // Generic attacks
                "You attack with your weapon",
                "Your weapon shine and request blood",
                "ATTAAAAAACK!",
                "BLOOD! BLOOD! BLOOD!",
                "Your Weapon is hungry for fresh flesh."
            };
            this.attributeAffinity = attraffinity;
        }

        /// <summary>
        /// Print an attack description plus the damage and return the damage you have done.
        /// </summary>
        /// <returns>The damage you have done with the dice as int</returns>
        public override int Use()
        {
            int d = damageDice.RollDice();

            UIHandler.PrintPositionedText(attacks[new Random().Next(0, attacks.Length-1)]); //Attack description
            UIHandler.PrintPositionedText("You have rolled: " + d, TextPosition.Center, ConsoleColor.Cyan);

            return d;
        }

        public override string ToString()
        {
            return base.ToString() + " ~ Damage Dice: " + damageDice + " ~ " + attributeAffinity;
        }

        public override string ToShortString()
        {
            return base.ToShortString() + " ~ " + damageDice;
        }
    }
}
