using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG.InventorySystem.Potions
{

    class HealPotion : Potion
    {
        Dice healDice;
        

        public HealPotion(string name, string desc, Currency value, PotionTypes potiontype, Dice healDice) : base(name, desc, value, potiontype)
        {
            this.healDice = healDice;
        }

        /// <summary>
        /// Use the potion roll the dice, decrease its usage and return the heal done
        /// </summary>
        /// <returns>the heal done</returns>
        public override int Use()
        {
            if(base.Use() > 0)
            {
                int dice = healDice.RollDice();

                UIHandler.PrintPositionedText("You have used " + Name + " and healed for " + dice);

                return dice;
            }

            return 0;
        }

        public override string ToString()
        {
            return base.ToString() + " ~ Heal Dice: " + healDice ;
        }

        public override string ToShortString()
        {
            return base.ToShortString() + " ~ " + healDice + " ♥";
        }
    }
}
