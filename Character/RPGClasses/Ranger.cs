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
    class Ranger : RPGClass
    {
        Bow huntBow;

        public Ranger() : base()
        {
            this.Art = new AsciiArt("AsciiArt.Ranger");

            Classtype = RPGClassTypes.Ranger;

            Description = "Warriors of the wilderness, rangers specialize in hunting the monsters\n" +
            "that threaten the edges of civilization—humanoid raiders, rampaging beasts and monstrosities,\n" +
            "terrible giants, and deadly dragons. They learn to track their quarry as a predator does,\n" +
            "moving stealthily through the wilds and hiding themselves in brush and rubble.\n" +
            "Rangers focus their combat training on techniques that are particularly\n" +
            "useful against their specific favored foes.";

            huntBow =
                new Bow(
                    "Hunting Bow",
                    "A good bow if you for the hunt",
                    new Currency(0, 0, 0),
                    2.0,
                    new Dice(DiceTypes.D8),
                    Statistic.Dexterity
                    ,15);

            HealPotion baseFlask =
                new HealPotion(
                    "Lichen Moss Drink",
                    "The default medicine for the Ranger",
                    new Currency(0, 0, 0),
                    PotionTypes.Flask,
                    new Dice(DiceTypes.D4));

            BaseKit.Add(huntBow);
            BaseKit.Add(baseFlask);

            StatBonus = Statistic.Dexterity;
        }

        public override int Skill(int id) //Implementazione delle diverse abilità per classe
        {
            return id switch
            {
                0 => PrecisionShot(),
                _ => 0,
            };
        }

        public int PrecisionShot() // Esempio
        {
            return 0;
        }
    }
}
