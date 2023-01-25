using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

using BasicRPG.RPGRaces;
using BasicRPG.UI;
using BasicRPG.InventorySystem;
using BasicRPG.GoldCurrency;
using BasicRPG.InventorySystem.Weapons;
using BasicRPG.Character.RPGClasses;
using BasicRPG.InventorySystem.Potions;

namespace BasicRPG.Character
{
    //[Serializable]
    class PlayerCharacter
    {
        string name;
        int age;

        HealthPoints hp;
        Race playerRace;
        RPGStatistics playerAttributes;

        RPGClass playerClass;
        Currency moneybag;
        Level level;

        Backpack backpack;
        
        public Backpack Backpack { get => backpack; set => backpack = value; }
        public Currency Gold { get => moneybag; set => moneybag = value; }
        public Level Level { get => level; }
        public string Name { get => name; }
        internal HealthPoints Hp { get => hp; set => hp = value; }
        internal RPGClass PlayerClass { get => playerClass; }

        public PlayerCharacter()
        {
            playerAttributes = new RPGStatistics();

            moneybag = new Currency(0, 5, 50);
            level = new Level();
            hp = new HealthPoints(0);
        }

        public void CharacterCreation()
        {
            UIHandler.PrintPositionedText("Welcome Adventurer! Let's Build your character.");

            UIHandler.PrintPositionedText("What's your name?");
            name = UIHandler.AskLine();
            
            age = UIHandler.SliderSelector(18, 100, "Select your age", ConsoleColor.Yellow);// Migliorabile usando l'età massima di ogni razza

            Console.Clear();
            PrintInfoAttributes();

            UIHandler.PressAnyKeyToContinue();
            GenerateAttributes();

            Console.Clear();

            SelectRace();

            Console.Clear();

            UIHandler.PrintPositionedText("These are your attributes: ");
            Console.WriteLine();
            playerAttributes.PrintAttributes();
            UIHandler.PressAnyKeyToContinue();

            SelectClass();

            Console.Clear();

            UIHandler.PrintPositionedText("Character Creation Completed.");
            Console.WriteLine();
            UIHandler.PrintPositionedText("This is your character:");
            Console.WriteLine();

            CalculateHitPoints();

            PrintCharacter();
            UIHandler.PressAnyKeyToContinue();
        }

        private void CalculateHitPoints()
        {
            // Il livello non conta piu visto che va a scalare in base alle costituzione
            hp = new HealthPoints(playerAttributes.Attributes[Statistic.Constitution] / 2);
        }

        public void PrintCharacter()
        {
            UIHandler.PrintPositionedText(
                "-> Name: " + name +
                " ~ Age: " + age +
                " ~ Race: " + playerRace.FRace +
                " ~ Class: " + playerClass.Classtype + " <-");

            UIHandler.PrintPositionedText(hp.ToString());
            UIHandler.PrintPositionedText(moneybag.ToString());
            UIHandler.PrintPositionedText(level.ToString());

            Console.WriteLine();
            playerRace.PrintRacialBonus();

            Console.WriteLine();

            UIHandler.PrintPositionedText("These are your attributes:");
            Console.WriteLine();

            playerAttributes.PrintAttributes();

            Console.WriteLine();
        }

        public void PrintInfoAttributes()
        {
            UIHandler.PrintPositionedText("Six Abilities provide a quick description of every creature’s physical and mental characteristics:");

            Console.WriteLine();
            UIHandler.PrintPositionedText("(STR) STRENGHT", TextPosition.Center, ConsoleColor.Red);
            UIHandler.PrintPositionedText("measuring physical power");
            Console.WriteLine();
            UIHandler.PrintPositionedText("(DEX) DEXTERITY", TextPosition.Center, ConsoleColor.Green);
            UIHandler.PrintPositionedText("measuring agility");
            Console.WriteLine();
            UIHandler.PrintPositionedText("(CON) CONSTITUTION", TextPosition.Center, ConsoleColor.DarkYellow);
            UIHandler.PrintPositionedText("measuring endurance");
            Console.WriteLine();
            UIHandler.PrintPositionedText("(INT) INTELLIGENCE", TextPosition.Center, ConsoleColor.Blue);
            UIHandler.PrintPositionedText("measuring reasoning and memory");
            Console.WriteLine();
            UIHandler.PrintPositionedText("(WIS) WISDOM", TextPosition.Center, ConsoleColor.Yellow);
            UIHandler.PrintPositionedText("measuring Perception and Insight");
            Console.WriteLine();
            UIHandler.PrintPositionedText("(CHA) CHARISMA", TextPosition.Center, ConsoleColor.DarkCyan);
            UIHandler.PrintPositionedText("measuring force of Personality");
            Console.WriteLine();
            Console.WriteLine();
            UIHandler.PrintPositionedText("Is a character muscle-bound and insightful? Brilliant and charming? Nimble and hardy? ");
            UIHandler.PrintPositionedText("Ability Scores define these qualities—a creature’s assets as well as weaknesses.");
            Console.WriteLine();
        }

        private void SelectClass()
        {
            do
            {
                Console.Clear();

                RPGClassTypes selectedClass = (RPGClassTypes)UIHandler.SelectiveChoice("Select your Character's Class", Enum.GetNames(typeof(RPGClassTypes)), TextPosition.Center);
                Console.Clear();

                playerClass = selectedClass switch
                {
                    RPGClassTypes.Warrior => new Warrior(),
                    RPGClassTypes.Ranger => new Ranger(),
                    RPGClassTypes.Bard => new Bard(),
                    RPGClassTypes.Wizard => new Wizard(),
                    _ => null
                };


                backpack = new Backpack(playerClass.BaseKit);
                playerClass.PrintRpgClass();

                Console.WriteLine();
            } while (UIHandler.PressSpecificKeyToContinue(ConsoleKey.Backspace, "Press [Backspace] to select an other class or any key to confirm your choice..."));
        }

        private void SelectRace()
        {
            do
            {
                Console.Clear();

                RaceTypes selectedRace = (RaceTypes)UIHandler.SelectiveChoice("Select your Character's Race", Enum.GetNames(typeof(RaceTypes)), TextPosition.Center);
                Console.Clear();


                playerRace = new Race(selectedRace);

                playerRace.PrintRacialBonus();
                Console.WriteLine("\n\n" + playerRace.Description + "\n\n");

            } while (UIHandler.PressSpecificKeyToContinue(ConsoleKey.Backspace, "Press [Backspace] to select an other race or any key to confirm your choice..."));

            playerAttributes += playerRace.Bonuses;
        }

        private void GenerateAttributes()
        {
            int selectedMethod =
                UIHandler.SelectiveChoice(
                    "Would you like to set these attributes by Point Buy or by Random?",
                    new string[] { "Point Buy", "Random Generation" },
                    TextPosition.Center);

            Console.Clear();

            switch (selectedMethod)
            {
                case 0:
                    playerAttributes.PointBuy();
                    break;

                case 1:
                    playerAttributes.RandomGeneration();
                    break;
            }

            
        }


        /// <summary>
        /// Add exp to the level of the player and if the level increases, the player must roll for increase his/her attributes. 
        /// </summary>
        /// <param name="exp">The exp to add</param>
        public void AddExperience(int exp) {
            int nl = level.AddExperience(exp); //add exp to the level of the player and return the numbers of levels increased

            if (nl > 0)
            {
                UIHandler.PrintPositionedText("Your Level Increased by " + nl + "!");
                UIHandler.PressAnyKeyToContinue("Your attributes increased and since you are a " + playerClass.GetType().Name + " you have a bonus on " + playerClass.StatBonus);


                playerAttributes.AddToAttributes(nl, nl, nl, nl, nl, nl);
                playerAttributes.AddToAttribute(playerClass.StatBonus, nl);


                CalculateHitPoints();
            }

            Console.Clear();
            UIHandler.PrintPositionedText("Congratulation! These are your new attributes");
            Console.WriteLine();
            playerAttributes.PrintAttributes();
            UIHandler.PressAnyKeyToContinue();
        }

        public int Attack(Weapon wep, out int bonusDamage)
        {
            int attraffbonus = (int)playerAttributes.Attributes[wep.AttributeAffinity] / 2;

            bonusDamage = attraffbonus;
            return wep.Use() + attraffbonus;
        }

        public void TakeDamage(int damagepoints)
        {
            UIHandler.PrintPositionedText(name + " takes " + damagepoints + " damage");
            if (!hp.Damage(damagepoints))
            {
                
                Death();
            }
        }

        public void HealPlayer(HealPotion pot, out int bonusHeal)
        {
            bonusHeal = playerAttributes.Attributes[Statistic.Wisdom] / 3;
            int heal = pot.Use();

            heal += bonusHeal;
            hp.Heal(heal);
        }

        public void Death()
        {
            UIHandler.PressAnyKeyToContinue(name + "'s poor life ended");
            Console.Clear();

            string dead =
                  "▓██   ██▓ ▒█████   █    ██    ▓█████▄  ██▓▓█████ ▓█████▄ \n"+
                  "▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▒██▀ ██▌▓██▒▓█   ▀ ▒██▀ ██▌ \n"+
                  "▒██ ██░▒██░  ██▒▓██  ▒██░   ░██   █▌▒██▒▒███   ░██   █▌  \n"+
                  "░ ▐██▓░▒██   ██░▓▓█  ░██░   ░▓█▄   ▌░██░▒▓█  ▄ ░▓█▄   ▌  \n"+
                  "░ ██▒▓░░ ████▓▒░▒▒█████▓    ░▒████▓ ░██░░▒████▒░▒████▓   \n"+
                  "██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒     ▒▒▓  ▒ ░▓  ░░ ▒░ ░ ▒▒▓  ▒    \n"+
                  "▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░     ░ ▒  ▒  ▒ ░ ░ ░  ░ ░ ▒  ▒  \n"+
                  "▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░     ░ ░  ░  ▒ ░   ░    ░ ░  ░  \n"+
                  "░ ░         ░ ░     ░           ░     ░     ░  ░   ░     \n"+
                  "░ ░                           ░                  ░       \n";

            Console.WriteLine("\n\n");
            UIHandler.PrintPositionedText(dead.Split("\n"), TextPosition.Center, ConsoleColor.DarkRed);
            UIHandler.PressAnyKeyToContinue();
            Environment.Exit(0); // Per adesso
        }


        /* salvare su file o su resources, se si puo fare?*/
    }
}



