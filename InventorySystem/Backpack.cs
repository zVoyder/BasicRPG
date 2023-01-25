using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPG.InventorySystem
{
    class Backpack : Inventory
    {
        const double defaultBackpackWeight = 40.0;

        double currentWeight, maxWeight; //How much weight can support your backpack

        public Backpack(double maxWeight = defaultBackpackWeight) : base()
        {
            this.maxWeight = maxWeight;
            currentWeight = 0;
        }

        public Backpack(Inventory inv, double maxWeight = defaultBackpackWeight)
        {
            this.maxWeight = maxWeight;
            currentWeight = 0;

            foreach(Item it in inv.Items)
            {
                Add(it);
            }
        }

        public new void Add(Item item)
        {
            if (CanContain(item))
            {
                currentWeight += item.Weight;
                Items.Add(item);
            }
            else
            {
                throw new Exception("This item is too heavy for this backpack now.");
            }
        }

        public bool CanContain(Item it)
        {
            if (maxWeight >= (currentWeight + it.Weight))
                return true;

            return false;
        }

        public override bool Remove(Item item)
        {
            if (base.Contains(item))
            {
                currentWeight -= item.Weight;

                return base.Remove(item);
            }

            return false;
        }
    }
}
