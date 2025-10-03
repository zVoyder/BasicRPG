using BasicRPG.InventorySystem;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BasicRPG.Character.RPGClasses
{
    public enum RPGClassTypes
    {
        Warrior,
        Ranger,
        Bard,
        // Wizard
    }
    
    abstract class RPGClass
    {
        Inventory baseKit;
        AsciiArt art;
        Statistic statBonus;

        public Inventory BaseKit { get => baseKit; set => baseKit = value; }
        public AsciiArt Art { get => art; set => art = value; }

        public string Description { get; set; }
        public RPGClassTypes Classtype { get; set; }
        public Statistic StatBonus { get => statBonus; set => statBonus = value; }

        public RPGClass()
        {
            BaseKit = new Inventory();
        }

        public abstract int Skill(int id);

        public void PrintRpgClass()
        {
            art.PrintAsciiArt();
            Console.WriteLine();
            Console.WriteLine(Description);
        }

    }
}
