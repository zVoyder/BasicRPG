using BasicRPG.GoldCurrency;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG.InventorySystem.Potions
{
    public enum PotionTypes
    {
        Impact = 1, // Same value but designly different
        Vial = 1,
        Flask = 3,
        Canteen = 5,
        Wineskin = 7,
        Barrel = 10
    }

    class Potion : Item
    {
        int usages, maxUsages;
        PotionTypes potiontype;

        public int Usages { get => usages; }

        public Potion(string name, string desc, Currency value, PotionTypes potiontype) : base(name, desc, value)
        {
            this.Weight = (int)potiontype; // because every usage (that is a liter) is equal to 1 kg => 1lt = 1kg.
            this.usages = (int)potiontype;
            this.maxUsages = usages;
            this.potiontype = potiontype;
        }

        

        /// <summary>
        /// For the base class its return is the remaining usages.
        /// </summary>
        /// <returns>The remaining usages of the potion</returns>
        public override int Use()
        {
            if(usages > 0) {
                usages--;
            }
            else
            {
                UIHandler.PrintPositionedText("Your " + Name + " is empty");
            }

            return Usages;
        }

        public override string ToString()
        {
            return base.ToString() + " ~ " + potiontype + " (" + usages + "/" + maxUsages + ")";
        }

        public override string ToShortString()
        {
            return base.ToShortString() + " ~ (" + usages + "/" + maxUsages + ")";
        }
    }
}
