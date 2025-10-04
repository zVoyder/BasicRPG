using BasicRPG.Character;
using BasicRPG.GoldCurrency;
using BasicRPG.UI;
using System;
using System.Collections.Generic;

namespace BasicRPG.InventorySystem
{
    using Weapons;

    class InventoryShop
    {
        Inventory weapons, potions;
        AsciiArt artshop;

        public Inventory Weapons { get => weapons; set => weapons = value; }
        public Inventory Potions { get => potions; set => potions = value; }

        public InventoryShop()
        {
            weapons = new Inventory("Data.Weapons"); // Gets the resources from the .resx file
            potions = new Inventory("Data.Potions");
            artshop = new AsciiArt("AsciiArt.Shop");
        }

        public void OpenShop(PlayerCharacter pl)
        {
            int t;

            do
            {
                Console.Clear();

                t = UIHandler.SelectiveChoice(artshop, "What are you looking for?", new string[]
                {
                    "Weapons",
                    "Potions",
                    "Buy Arrows",
                    "*Go Away*"
                });

                List<string> wepstrs = weapons.ToStrings();
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

                        BuyItem(pl, weapons.Items[i]);
                        Console.Clear();
                        break;

                    case 1:
                        i = UIHandler.SelectiveChoice("What do you like? \n You have " + pl.Gold, potstrs.ToArray());

                        if (i == potstrs.Count - 1)
                            break;

                        BuyItem(pl, potions.Items[i]);
                        Console.Clear();
                        break;

                    case 2:
                        List<Bow> bows = GetBows(pl);
                        
                        if (bows.Count == 0)
                        {
                            UIHandler.PressAnyKeyToContinue("You don't have any bows. Come back when you have one!");
                            break;
                        }
                        
                        Currency price = new Currency(0, 0, 1);
                        price += new Currency(0, 0, 2 * pl.Level.NumberLevel);
                        
                        Console.Clear();
                        
                        int maxLoadable = 0;
                        foreach (Bow bow in GetBows(pl))
                        {
                            maxLoadable += bow.MaxArrows - bow.Arrows;
                            
                            if (bow.Arrows < bow.MaxArrows)
                                UIHandler.PrintPositionedText(bow.ToString());
                        }

                        int maxAffordable = (pl.Gold.ToDecimal() / price.ToDecimal());
                        int maxArrows = Math.Min(maxLoadable, maxAffordable);
                        
                        UIHandler.PressAnyKeyToContinue("These are your bows that can be reloaded. You have " + pl.Gold + ". Each arrow costs " + price + ". Press any key to continue...");
                        int count = UIHandler.SliderSelector(1, maxArrows, "How many arrows do you want to buy? (1 arrow = " + price + ")");
                        BuyArrows(pl, price, count);
                        break;

                    case 3:
                        Console.Clear();
                        artshop.PrintAsciiArt();
                        UIHandler.PressAnyKeyToContinue("Come back when you want!");
                        break;
                }

                Console.Clear();
            } while (t != 3);
        }

        /// <summary>
        /// Check if the gold is enough to buy it
        /// </summary>
        /// <param name="gold">The gold i want to use to pay the item</param>
        /// <param name="it">The item</param>
        /// <returns>Returns true if it can, false if it cannot</returns>
        public void BuyItem(PlayerCharacter pl, Item it)
        {
            if (pl.Gold >= it.Value)
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

        public void BuyArrows(PlayerCharacter pl, Currency price, int count)
        {
            if (pl.Gold >= price * count)
            {
                List<Bow> bows = GetBows(pl);

                bows.Sort((Bow a, Bow b) => (b.Value - a.Value).ToDecimal());
                int unloaded = count;
                foreach (Bow bow in bows)
                {
                    int loadedAmmo = bow.RefillArrows(unloaded);
                    unloaded -= loadedAmmo;
                }
                
                pl.Gold -= price;
                UIHandler.PressAnyKeyToContinue("You have bought " + count + " arrows!");
            }
            else
            {
                UIHandler.PressAnyKeyToContinue("You don't have enough money to buy " + count + " arrows.");
            }
        }
        
        private List<Bow> GetBows(PlayerCharacter pl)
        {
            List<Bow> bows = new List<Bow>();
            foreach (Item item in pl.Backpack.Items)
            {
                if (item is Weapons.Bow bow)
                    bows.Add(bow);
            }
            return bows;
        }
    }
}