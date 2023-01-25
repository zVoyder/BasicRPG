using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.Character.RPGClasses;
using System;
using System.Collections.Generic;
using System.Text;
using BasicRPG.Character;

namespace BasicRPG.InventorySystem.Weapons
{
    class Sword : InventorySystem.Weapons.Weapon
    {
        readonly string[] attackDescriptions = {
            "You look the enemy dead in the eye as you draw your blade and attack.\n",
            "As You brush off the sweat streaming down your face, you take a moment to center yourself and focus before you swing towards the enemy.\n",
            "As rage coursing through your veins, you charge your opponent.\n",
            "You hold your sword firmly and attack your enemy.\n",
            "You perform a sword thrust, trying to skewer your enemy.\n",
            "While facing your enemy, you perform an overhead sword slash.\n"
        };

        public Sword(string name, string desc, Currency value, double weight, Dice damageDice, Statistic attraffinity) : base(name, desc, value, weight, damageDice, attraffinity)
        {
            attacks = attackDescriptions;
        }

        public override int Use()
        {
            return base.Use();
        }

    }
}
