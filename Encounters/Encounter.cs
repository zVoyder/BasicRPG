using BasicRPG.Character;
using BasicRPG.GoldCurrency;
using BasicRPG.InventorySystem;
using BasicRPG.InventorySystem.Potions;
using BasicRPG.InventorySystem.Weapons;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG.Encounters
{
    static class Encounter
    {
        /// <summary>
        /// Start a fight between the player and the enemy
        /// </summary>
        /// <param name="player"> The player </param>
        /// <param name="enemy"> The enemy </param>
        /// <returns></returns>
        public static void Fight(PlayerCharacter player, Enemy enemy)
        {
            Console.Clear();

            UIHandler.PrintPositionedText("A fight starts between " + player.Name + " and " + enemy.Name);
            UIHandler.PressAnyKeyToContinue();

            bool enemyIsDead = false;

            do
            {
                AsciiArt playerVSenemy = AsciiArt.ModifyEndline(player.PlayerClass.Art, player.Name + " " + player.Hp.ToString()) + AsciiArt.ModifyEndline(enemy.Art, enemy.Name + " " + enemy.Hp.ToString());
                
                #region Player Turn

                playerVSenemy = AsciiArt.ModifyEndline(player.PlayerClass.Art, player.Name + " " + player.Hp.ToString()) + AsciiArt.ModifyEndline(enemy.Art, enemy.Name + " " + enemy.Hp.ToString());
                int x = UIHandler.SelectiveChoice(playerVSenemy, " \n \n It's " + player.Name + "'s turn, choose your next action:", player.Backpack.ToShortStrings().ToArray());

                //Console.WriteLine("you selected " + player.Backpack.Items[x].ToShortString());

                Console.Clear();
                playerVSenemy.PrintAsciiArt();
                Console.WriteLine();

                Item usedItem = player.Backpack.Items[x];
                if (usedItem is Weapon) // Cannot use the switch due to IS (i tried)
                {
                    Weapon usedWep = (Weapon)usedItem;
                    int damage = player.Attack(usedWep, out int bonusdmg);

                    if (bonusdmg > 0)
                        UIHandler.PrintPositionedText("+" + bonusdmg + " for " + usedWep.AttributeAffinity + " Weapon Affinity");

                    enemyIsDead = enemy.TakeDamage(damage);
                }
                else if (usedItem is Potion) {

                    Potion pot = (Potion)player.Backpack.Items[x];
                    
                    if(pot is HealPotion)
                    {
                        player.HealPlayer((HealPotion)pot, out int bonusHeal);
                        UIHandler.PrintPositionedText("+" + bonusHeal + " for your Wisdom");
                    }
                }

                Console.WriteLine();

                #endregion
                                
                UIHandler.PressAnyKeyToContinue();
                
                if (enemyIsDead)
                    break;
                
                #region Enemy Turn

                Console.Clear();
                playerVSenemy.PrintAsciiArt();
                Console.WriteLine();
                UIHandler.PressAnyKeyToContinue("It's " + enemy.Name + "'s turn, press any key to continue...");
                
                Console.Clear();

                enemy.Art.PrintAsciiArt();
                player.TakeDamage(enemy.Attack());

                #endregion
                
                UIHandler.PressAnyKeyToContinue(player.Name + " remains with " + player.Hp);
                Console.Clear();

            } while (!enemyIsDead);
            
            Console.Clear();
            Currency reward = enemy.GetReward(out int exp);
            
            enemy.Art.PrintAsciiArt();
            UIHandler.PrintPositionedText(enemy.Hp.ToString());
            enemy.Death();

            UIHandler.PrintPositionedText(enemy.Name + " is defeated");
            UIHandler.PrintPositionedText(player.Name + " gains " + reward + " and " + exp + " Experience!");

            UIHandler.PressAnyKeyToContinue("Your enemy is defeated press any key to continue...");

            player.AddExperience(exp);
            player.Gold += reward;
        }
    }
}
