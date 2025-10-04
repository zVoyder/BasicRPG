using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.InventorySystem.Potions;
using BasicRPG.InventorySystem.Weapons;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG.Character.RPGClasses
{
    class Bard : RPGClass
    {
        BattleInstrument baseLute;
        public Bard() : base()
        {
            this.Art = new AsciiArt("AsciiArt.Bard");

            Classtype = RPGClassTypes.Bard;

            Description = "The bard is a master of song, speech, and the magic they contain.\n" +
            "Bards say that the multiverse was spoken into existence, that the words of the gods gave it shape\n" +
            "and that echoes of these primordial Words of Creation still resound throughout the cosmos.\n" +
            "The music of bards is an attempt to snatch and harness those echoes, subtly woven into their spells and powers.";

            baseLute = new BattleInstrument(
                    "Old Lute",
                    "An old lute that can still strum a few notes.",
                    new Currency(0, 0, 0),
                    4.0,
                    new Dice(DiceTypes.D7),
                    Statistic.Charisma);

            HealPotion baseFlask =
                new HealPotion(
                    "Dancing Rose Scent",
                    "The default medicine and perfume of the Bard",
                    new Currency(0, 0, 0),
                    PotionTypes.Flask,
                    new Dice(DiceTypes.D4));

            BaseKit.Add(baseLute);
            BaseKit.Add(baseFlask);

            StatBonus = Statistic.Charisma;
        }

        public override int Skill(int id) //Implementazione delle diverse abilità per classe
        {
            return id switch
            {
                0 => Melody(),
                _ => 0,
            };
        }

        public int Melody() // Esempio
        {
            return 0;
        }
    }
}
