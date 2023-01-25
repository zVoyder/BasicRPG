using System;
using System.Collections.Generic;
using System.Text;
using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.InventorySystem;
using BasicRPG.InventorySystem.Weapons;
using BasicRPG.UI;

namespace BasicRPG.Character.RPGClasses
{
    class Wizard : RPGClass
    {
        

        public Wizard() : base()
        {
            this.Art = new AsciiArt("AsciiArt.Wizard");

            Classtype = RPGClassTypes.Wizard;

            Description = "Wild and enigmatic, varied in form and function,\n" +
                "the power of magic draws students who seek to master its mysteries.\n" +
                "Some aspire to become like the gods, shaping reality itself.\n" +
                "Though the casting of a typical spell requires merely the utterance of a few strange words,\n" +
                "fleeting gestures, and sometimes a pinch or clump of exotic materials, these surface components barely hint\n" +
                "at the expertise attained after years of apprenticeship and countless hours of study.";

            /*
            BaseKit.Add(
                new Weapon( //per adesso
                    "Scholar's Staff",
                    "Each student's accademic staff",
                    new Currency(0, 0, 0),
                    0.2,
                    new Dice(DiceTypes.D10),
                    "-==--o",
                    RPGClassTypes.Wizard)
                );*/

            StatBonus = Statistic.Intelligence;
        }

        public override int Skill(int id) //Implementazione delle diverse abilità per classe
        {
            return id switch
            {
                0 => FireBall(),
                _ => 0,
            };
        }

        public int FireBall() // Esempio
        {
            return 0;
        }
    }
}
