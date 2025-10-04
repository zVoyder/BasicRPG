using System;
using System.Collections.Generic;
using System.Text;
using BasicRPG.InventorySystem;
using BasicRPG.GoldCurrency;
using BasicRPG.Dices;
using BasicRPG.InventorySystem.Weapons;
using BasicRPG.UI;
using System.IO;
using System.Reflection;
using BasicRPG.InventorySystem.Potions;

namespace BasicRPG.Character.RPGClasses
{
    class Warrior : RPGClass
    {

        public Warrior() : base()
        {
            this.Art = new AsciiArt("AsciiArt.Warrior");

            Classtype = RPGClassTypes.Warrior;

            Description = "Warriors come alive in the chaos of combat.\n" +
                "They can enter a berserk state where rage takes over, giving them superhuman strength and resilience.\n" +
                "A Warrior can draw on this reservoir of fury only a few times without resting, but\n" +
                "those few rages are usually sufficient to defeat whatever threats arise.";

            Sword defaultsword =
                new Sword(
                    "Shortsword",
                    "Short and handy",
                    new Currency(),
                    4.0,
                    new Dice(DiceTypes.D4),
                    Statistic.Strength);

            HealPotion baseFlask = 
                new HealPotion(
                    "Vigor Flask",
                    "The default medicine for the warrior",
                    new Currency(0,0,0),
                    PotionTypes.Flask,
                    new Dice(DiceTypes.D4));

            BaseKit.Add(defaultsword);
            BaseKit.Add(baseFlask);

            StatBonus = Statistic.Strength;
        }

        public override int Skill(int id) //Implementazione delle diverse abilità per classe
        {
            return id switch
            {
                0 => WarCry(),
                _ => 0,
            };
        }

        public int WarCry() // Esempio
        {
            return 0;
        }
    }
}
