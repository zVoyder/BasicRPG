using BasicRPG.Character;
using BasicRPG.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG.RPGRaces
{
    public enum RaceTypes
    {
        Human,
        Elf,
        Dwarf,
        Dragonborn
    }

    [Serializable]
    class Race
    {
        string description;

        RPGStatistics bonuses;

        public string Description { get => description; }

        RaceTypes race;

        public RaceTypes FRace { get => race; }
        public RPGStatistics Bonuses { get => bonuses; set => bonuses = value; }

        public Race(RaceTypes racetype)
        {
            race = racetype;

            switch (racetype)
            {
                case RaceTypes.Human:
                    bonuses = new RPGStatistics(2, 2, 2, 2, 2, 2);

                    description = "Humans are the most adaptable and ambitious people among the common races.\n" +
                        "They have widely varying tastes, morals, and customs in the many different lands\n" +
                        "where they have settled.\n" +
                        "When they settle, though, they stay: they build cities to last for the ages,\n" +
                        "and great kingdoms that can persist for long centuries.";

                break;

                case RaceTypes.Elf:
                    bonuses = new RPGStatistics(0, 4, 0, 2, 0, 0);

                    description = "With their unearthly grace and fine features,\n" +
                        "elves appear hauntingly beautiful to humans and members of many other races.\n" +
                        "Elves’ coloration encompasses the normal human range and also includes skin in shades of\n" +
                        "copper, bronze, and almost bluish-white, hair of green or blue, and eyes like pools of liquid gold or silver.\n" +
                        "Elves have no facial and little body hair.\n" +
                        "They favor elegant clothing in bright colors, and they enjoy simple yet lovely jewelry.\n" +
                        "Elves are a magical people of otherworldly grace, living in the world but not entirely part of it.\n" +
                        "They live in places of ethereal beauty, in the midst of ancient forests\n" +
                        "or in silvery spires glittering with faerie light,\n" +
                        "where soft music drifts through the air and gentle fragrances waft on the breeze.\n" +
                        "Elves love nature and magic, art and artistry, music and poetry, and the good things of the world.";
                    break;

                case RaceTypes.Dwarf:
                    bonuses = new RPGStatistics(0, 0, 4, 0, 2, 0);

                    description = "Bold and hardy, dwarves are known as skilled warriors,\n" +
                        "miners, and workers of stone and metal.\n" +
                        "Though they stand well under 5 feet tall, dwarves are so broad\n" +
                        "and compact that they can weigh as much as a human standing nearly two feet taller.\n" +
                        "Their courage and endurance are also easily a match for any of the larger folk.";
                    break;

                case RaceTypes.Dragonborn:
                    bonuses = new RPGStatistics(4, 0, 0, 0, 0, 2);

                    description = "Dragonborns are bipedal creatures, resembling a dragon in humanoid form.\n" +
                        "A dragonborn's skin is covered in fine scales, giving it a leathery texture.\n" +
                        "Larger scales appear on the forearms, lower legs, feet, thighs, and shoulders.\n" +
                        "The color of a dragonborn's scales varies, but commonly a dull metallic shade, such as scarlet, gold, rust, ochre, bronze, brown,\n" +
                        "or copper-green. Brass and bronze are the most common shades.\n" +
                        "Dragonborn are a proud and strong-willed race, with a strong sense of honor. \n" +
                        "Tradition and clan ties are important. A dragonborn's honor is more important than their own life.\n" +
                        "Dragonborn are expected to act chivalrously on the battlefield, and show the proper respect even to enemies.\n" +
                        "Cowardice and oathbreaking are the most egregious sins a dragonborn can commit.\n" +
                        "Honor requires that they take responsibility for their actions, speak honestly, and honor their commitments.";
                    break;
            }
        }

        public void PrintRacialBonus()
        {
            UIHandler.PrintPositionedText("You have the following bonuses since you are " + race + "\n");

            foreach(KeyValuePair<Statistic, int> kp in bonuses.Attributes)
            {
                UIHandler.PrintPositionedText(kp.Key.ToString().Substring(0, 3).ToUpper() + "    (" + kp.Value.ToString("+0;-0") + ")");
            }
        }
    }
}
