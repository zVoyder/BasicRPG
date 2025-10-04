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
        public Dice healDice;
        
        public HealPotion(string name, string desc, Currency value, PotionTypes potiontype, Dice healDice) : base(name, desc, value, potiontype)
        {
            this.healDice = healDice;
        }

        protected override void OnUsed()
        {
            int dice = healDice.RollDice();
            UIHandler.PrintPositionedText("You have rolled: " + dice, TextPosition.Center, ConsoleColor.Cyan);
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
