using System;
using System.Collections.Generic;
using TextbasedRPG.Core.Items;
using TextbasedRPG.Exceptions;
using TextbasedRPG.Extensions;

namespace TextbasedRPG.Core
{
    /// <summary>
    /// Represents a basic container of <see cref="Item"/>s
    /// </summary>
    public class Inventory
    {
         private List<Item> items = new();

        public Inventory(IEnumerable<Item> startingItems)
        {
            items.AddRange(startingItems);
        }
        
        /// <summary>
        /// Used to help locate items that are ideally, not at their max capacity.
        /// </summary>
        /// <param name="name">Name of item to search for</param>
        /// <param name="allowMaxed">Are we allowing items that are at capacity? (default false)</param>
        /// <returns>If found, an item. Otherwise null</returns>
        private Item FindItem(string name, bool allowMaxed=false)
        {
            foreach(Item item in items)
                if (item.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (item.ItemCount == item.StackSize)
                    {
                        if (allowMaxed)
                            return item;
                        continue;
                    }

                    return item;
                }

            return null;
        }

        /// <summary>
        /// Adds <paramref name="item"/> to inventory
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            int amountToAdd = item.ItemCount;

            do
            {
                Item result = FindItem(item.Name);

                if (result == null)
                {
                    items.Add(item);
                    return;
                }

                if (result.ItemCount + amountToAdd <= result.StackSize)
                {
                    result.ItemCount += amountToAdd;
                    amountToAdd = 0;
                }
                else
                {
                    amountToAdd = (result.StackSize - result.ItemCount);
                    result.ItemCount = result.StackSize;
                }
            } while (amountToAdd > 0);
        }

        /// <summary>
        /// Case 1. Returns the entire stack (if amount >= item count)
        /// Case 2. Returns a clone of item with the specified amount
        /// </summary>
        /// <param name="name">Name of item to remove</param>
        /// <param name="amount">Amount of said item to retrieve</param>
        /// <returns>Item if available, otherwise exception</returns>
        /// <exception cref="ItemNotFoundException">Thrown when item could not be located in inventory</exception>
        /// <exception cref="ExceedsItemStackSizeException">Thrown when specified amount exceeds the item's stack size</exception>
        public Item RemoveItem(string name, int amount = 1)
        {
            Item result = FindItem(name, true);

            if (result == null)
                throw new ItemNotFoundException(name);

            if (amount > result.StackSize)
                throw new ExceedsItemStackSizeException(name, amount);
            
            if (amount >= result.ItemCount)
            {
                items.Remove(result);
                return result;
            }

            Item clone = result.Clone<Item>();
            clone.ItemCount = amount;
            result.ItemCount -= amount;

            return clone;
        }

        public override string ToString()
        {
            return items.ToTable(new[]
                {
                    "Name",
                    "Description",
                    "Price",
                    "Amount"
                },
                x => x.Name,
                x => x.Description,
                x => x.Price,
                x => $"{x.ItemCount} / {x.StackSize}\n");
        }
    }
}