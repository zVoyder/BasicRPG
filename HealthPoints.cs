using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG
{
    class HealthPoints
    {
        int currentHealth, maxHealth;
        char symbol;
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        
        public int CurrentHealth { get => currentHealth; }

        public HealthPoints(int maxHealth = 100, char symbol = '♥')
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            this.symbol = symbol;
        }

        /// <summary>
        /// Set the current hp to the Max hp
        /// </summary>
        public void Refresh()
        {
            currentHealth = maxHealth;
        }

        /// <summary>
        /// Substract damage to the healthpoints
        /// </summary>
        /// <param name="damagepoints"></param>
        /// <returns>return true if it can take the damage, false if it cannot take the damage</returns>
        public bool Damage(int damagepoints)
        {
            if (damagepoints < currentHealth)
            {
                currentHealth -= damagepoints;
                return true;
            }

            currentHealth = 0;
            return false;
        }

        public void Heal(int healpoints)
        {
            if (currentHealth + healpoints > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += healpoints;
            }
        }

        public override string ToString()
        {
            return symbol + " " + currentHealth + "|" + maxHealth;
        }

        public static HealthPoints operator +(HealthPoints a, HealthPoints b)
            => new HealthPoints(a.maxHealth + b.maxHealth);

    }
}
