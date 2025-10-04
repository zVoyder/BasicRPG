using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicRPG.GoldCurrency
{
    class Currency
    {
        int gold, silver, bronze;

        public int Gold { get => gold; }
        public int Silver { get => silver; }
        public int Bronze { get => bronze; }

        /// <summary>
        /// New Currency of 0G, 0S, 0B
        /// </summary>
        public Currency()
        {
            this.gold = 0;
            this.silver = 0;
            this.bronze = 0;
        }

        public Currency(int gold, int silver, int bronze)
        {
            if (gold < 0 || silver < 0 || bronze < 0)
            {
                throw new Exception("Error: Negative numbers");
            }

            this.gold = gold;
            AddSilver(silver);
            AddBronze(bronze);
        }

        public Currency(int @decimal)
        {
            if (@decimal < 0)
            {
                throw new Exception("Error: Negative numbers");
            }

            AddBronze(@decimal);
        }

        public void PrintCurrency()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return "[G]" + gold + " [S]" + silver + " [B]" + bronze;
        }
        
        public static Currency operator *(Currency a, int n)
        {
            return new Currency(a.Gold * n, a.Silver * n, a.Bronze * n);
        }

        public static Currency operator +(Currency a, Currency b)
        {
            return new Currency(a.Gold + b.Gold, a.Silver + b.Silver, a.Bronze + b.Bronze);
        }

        public static Currency operator -(Currency a, Currency b)
        {
            int nc = 0;

            if (a > b)
                nc = a.ToDecimal() - b.ToDecimal();

            return new Currency(nc);
        }

        public static Currency operator /(Currency a, int n)
        {
            int nc;

            nc = a.ToDecimal() / 2;

            return new Currency(nc);
        }

        public static bool operator >(Currency a, Currency b)
        {
            int n1 = a.ToDecimal();
            int n2 = b.ToDecimal();

            return n1 > n2;
        }

        public static bool operator <(Currency a, Currency b)
        {
            return !(a > b);
        }

        public override bool Equals(Object a)
        {
            return this == (Currency)a;
        } // I must define these methods since i use == and !=

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator !=(Currency a, Currency b)
        {
            return !(a == b);
        }

        public static bool operator ==(Currency a, Currency b)
        {
            if (a.gold == b.gold && a.silver == b.silver && a.bronze == b.bronze)
            {
                return true;
            }
            return false;
        }

        public static bool operator <=(Currency a, Currency b)
        {
            return (a == b || a < b);
        }

        public static bool operator >=(Currency a, Currency b)
        {
            return (a == b || a > b);
        }

        public void AddGold(int gold)
        {
            this.gold += gold;
        }

        public void AddSilver(int silver)
        {
            Operation(silver * 100);
        }

        public void AddBronze(int bronze)
        {
            Operation(bronze);
        }

        private void Operation(int n, int nbase = 100)
        {
            int[] coins = new int[2];
            int r;

            for (int i = 0; i < 2; i++)
            {
                r = n % nbase;
                coins[i] = r;
                n /= nbase;
            }

            gold += n;
            silver += coins[1];
            bronze += coins[0];
        }

        public int ToDecimal()
        {
            int g = gold * 10000;
            int s = silver * 100;
            int b = bronze;

            return g + s + b;
        }
    }
}