using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG.Character
{
    class Level
    {
        const int nextExpFactor = 100;

        int numberLevel;
        int experience, expForNextLevel;

        public float Experience { get => experience; }
        public int NumberLevel { get => numberLevel; }
        public int ExpForNextLevel { get => expForNextLevel; }

        public Level(int level = 1)
        {
            this.numberLevel = level;
            experience = 0;
            expForNextLevel = CalculateNextNeededExp();
        }

        /// <summary>
        /// Add experience and return the numbers of increased levels
        /// </summary>
        /// <param name="exp">experience to add</param>
        /// <returns>number of increased levels</returns>
        public int AddExperience(int exp)
        {
            int numberOfIncreasedLevels = 0;

            while (this.experience + exp >= expForNextLevel)
            {
                numberLevel++;
                numberOfIncreasedLevels++;
                exp -= expForNextLevel;
                expForNextLevel = CalculateNextNeededExp();
            }

            experience += exp;
            return numberOfIncreasedLevels;
        }

        int CalculateNextNeededExp()
        {
            return numberLevel * nextExpFactor;
        }

        public override string ToString()
        {
            return "Level: " + numberLevel + " Exp: " + experience + "/" + expForNextLevel;
        }
    }
}
