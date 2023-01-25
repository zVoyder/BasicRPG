using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.Character.RPGClasses;
using BasicRPG.Music;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using BasicRPG.Character;

namespace BasicRPG.InventorySystem.Weapons
{
    class BattleInstrument : Weapon
    {
        readonly string[] attackDescriptions = {
            "A sweet melody falls upon the enemies.\n",
            "A decisive melody overwhelms the enemies.\n",
            "Fiery notes slash at enemies like knives.\n"
        };

        public BattleInstrument(string name, string desc, Currency value, double weight, Dice damageDice, Statistic attraffinity) 
            : base(name, desc, value, weight, damageDice, attraffinity)
        {
            attacks = attackDescriptions;
        }

        public override int Use()
        {
            int n = base.Use();

            new Thread(() => PlayAttackSound()).Start();

            return n;
        }


        const int scalesnumbers = 3;
        public void PlayAttackSound()
        {
            string resname = "Music.BardScale" + new Random().Next(1, scalesnumbers);

            MusicSheet sound = new MusicSheet(resname);

            sound.Play();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
