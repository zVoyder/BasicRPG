using BasicRPG.Character;
using BasicRPG.Dices;
using BasicRPG.GoldCurrency;
using BasicRPG.InventorySystem.Potions;
using BasicRPG.InventorySystem.Weapons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BasicRPG.InventorySystem
{
    class Inventory
    {
        private List<Item> items;

        public List<Item> Items { get => items; set => items = value; }

        public Inventory()
        {
            items = new List<Item>();
        }

        public Inventory(List<Item> myinv)
        {
            this.items = myinv;
        }

        public Inventory(string resourcename)
        {
            items = new List<Item>();

            string[] itemlns = Properties.Resources.ResourceManager.GetString(resourcename).Split("\n");
            
            foreach (string itemln in itemlns.Where(s => s != "\r" && s != ""))
            {
                string[] splittedItem = itemln.Split("\t");


                string[] coststrings = splittedItem[3].Split("-");
                Currency cost = new Currency(int.Parse(coststrings[0]), int.Parse(coststrings[1]), int.Parse(coststrings[2]));


                Item it = splittedItem[0] switch
                {
                    "Sword" => new Sword(splittedItem[1], splittedItem[2], cost, double.Parse(splittedItem[4]), new Dice(Enum.Parse<DiceTypes>(splittedItem[5])), Enum.Parse<Statistic>(splittedItem[6])),
                    "Spear" => new Spear(splittedItem[1], splittedItem[2], cost, double.Parse(splittedItem[4]), new Dice(Enum.Parse<DiceTypes>(splittedItem[5])), Enum.Parse<Statistic>(splittedItem[6])),
                    "Bow" => new Bow(splittedItem[1], splittedItem[2], cost, double.Parse(splittedItem[4]), new Dice(Enum.Parse<DiceTypes>(splittedItem[5])), Enum.Parse<Statistic>(splittedItem[6]), int.Parse(splittedItem[7])),
                    "BattleInstrument" => new BattleInstrument(splittedItem[1], splittedItem[2], cost, double.Parse(splittedItem[4]), new Dice(Enum.Parse<DiceTypes>(splittedItem[5])), Enum.Parse<Statistic>(splittedItem[6])),

                    "HealPotion" => new HealPotion(splittedItem[1], splittedItem[2], cost, Enum.Parse<PotionTypes>(splittedItem[4]), new Dice(Enum.Parse<DiceTypes>(splittedItem[5]))),
                    _ => null,
                };

                items.Add(it);
            }
        }

        public virtual void Add(Item item)
        {
            items.Add(item);
        }
        
        public virtual bool Remove(Item item)
        {
            return items.Remove(item);
        }

        public bool Contains(Item item)
        {
            return items.Contains(item);
        }

        public void PrintInventoryItems()
        {
            foreach(Item it in items)
            {
                Console.WriteLine("" + it + " ]");
            }
        }

        public List<string> ToStrings()
        {
            List<string> stritems = new List<string>();

            foreach (Item it in items)
            {
                stritems.Add(it.ToString());
            }

            return stritems;
        }

        public List<string> ToShortStrings()
        {
            List<string> stritems = new List<string>();

            foreach (Item it in items)
            {
                stritems.Add(it.ToShortString());
            }

            return stritems;
        }
    }
}
