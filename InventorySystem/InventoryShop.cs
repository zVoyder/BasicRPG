using BasicRPG.Character;
using BasicRPG.GoldCurrency;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BasicRPG.InventorySystem
{
    class InventoryShop
    {
        Inventory weapons, potions;
        AsciiArt artshop;

        public Inventory Weapons { get => weapons; set => weapons = value; }
        public Inventory Potions { get => potions; set => potions = value; }

        public InventoryShop()
        {
            weapons = new Inventory("Data.Weapons"); // vado a prendere tutte le weapons
            potions = new Inventory("Data.Potions");
            artshop = new AsciiArt("AsciiArt.Shop"); // <- modo per implementare le risorse
            
        }

        //Fare acquisto e aggiunta e vendita

        public void OpenShop(PlayerCharacter pl)
        {
            int t;

            do
            {
                Console.Clear();

                t = UIHandler.SelectiveChoice(artshop, "What are you looking for?", new string[] { "Weapons", "Potions", "*Go Away*" });


                List<string> wepstrs = weapons.ToStrings(); // faccio così perchè vado ad aggiungere un modo per tornare indietro
                wepstrs.Add("Cancel");

                List<string> potstrs = potions.ToStrings();
                potstrs.Add("Cancel");

                int i;
                switch (t)
                {
                    case 0:
                        i = UIHandler.SelectiveChoice("What do you want to buy?\n You have " + pl.Gold, wepstrs.ToArray());

                        if (i == wepstrs.Count - 1)
                            break;

                        Buy(pl, weapons.Items[i]);

                        Console.Clear();

                        break;

                    case 1:
                        i = UIHandler.SelectiveChoice("What do you like? \n You have " + pl.Gold, potstrs.ToArray());

                        if (i == potstrs.Count - 1)
                            break;

                        Buy(pl, potions.Items[i]);

                        Console.Clear();

                        break;

                    case 2:
                        Console.Clear();

                        artshop.PrintAsciiArt();
                        UIHandler.PressAnyKeyToContinue("Come back when you want!");

                        break;
                }

                Console.Clear();
            } while (t != 2);
        }

        
        /// <summary>
        /// Check if the gold is enough to buy it
        /// </summary>
        /// <param name="gold">The gold i want to use to pay the item</param>
        /// <param name="it">The item</param>
        /// <returns>Returns true if it can, false if it cannot</returns>
        public void Buy(PlayerCharacter pl, Item it)
        {
            if(pl.Gold >= it.Value) 
            {
                if (pl.Backpack.CanContain(it))
                {
                    pl.Gold -= it.Value;
                    it.Value /= 2;

                    pl.Backpack.Add(it);

                    UIHandler.PressAnyKeyToContinue("You have bought " + it.Name + "!");
                }
                else
                {
                    UIHandler.PressAnyKeyToContinue("This item is too heavy for your backpack.");
                }
            }
            else
            {
                UIHandler.PressAnyKeyToContinue("You don't have enough money to buy " + it.Name);
            }
        }
    }
}
