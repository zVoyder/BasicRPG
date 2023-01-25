using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using BasicRPG.GoldCurrency;

namespace BasicRPG.InventorySystem
{
    abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Currency Value { get; set; }
        public double Weight { get; set; }

        public Item(string name, string desc, Currency value, double weight) 
        {
            Name = name;
            Description = desc;
            Value = value;
            Weight = weight;
        }

        public Item(string name, string desc, Currency value) // For potions i set the weight based off the potion size
        {
            Name = name;
            Description = desc;
            Value = value;
        }

        public override string ToString()
        {
            return  Value + " " + Name + " ~ " + Description + " ~ " + Weight + "Kg";
        }

        /// <summary>
        /// Short version of tostring to permit the override of the short versions of tostrings of the childs
        /// </summary>
        /// <returns></returns>
        public virtual string ToShortString()
        {
            return Name;
        }

        public abstract int Use();

    }
}
