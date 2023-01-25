using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.Character.RPGClasses;
using System;
using System.Collections.Generic;
using System.Text;
using BasicRPG.Character;

namespace BasicRPG.InventorySystem.Weapons
{
    class Spear : InventorySystem.Weapons.Weapon
    {
        readonly string[] attackDescriptions = {
            "You look the enemy dead in the eye as you hold your spear and attack.\n",
            "You take a moment to center yourself and focus before you thrust your spear towards the enemy.\n",
            "You hold your spear firmly and attack your enemy.\n",
            "You try to skewer your enemy.\n",
            "While facing your enemy, you perform an overhead spear slash.\n",
        };

        public Spear(string name, string desc, Currency value, double weight, Dice damageDice, Statistic attraffinity) : base(name, desc, value, weight, damageDice, attraffinity)
        {
            attacks = attackDescriptions;
        }

        public override int Use()
        {
            return base.Use();
        }
    }
}
