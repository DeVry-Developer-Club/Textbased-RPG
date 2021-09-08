using System;
using TextbasedRPG.Core;

namespace TextbasedRPG.Exceptions
{
    /// <summary>
    /// Thrown when an item is not found in <see cref="Inventory"/>
    /// </summary>
    public class ItemNotFoundException : Exception
    {
        public string ItemName { get; }

        public ItemNotFoundException(string name)
        {
            ItemName = name;
        }
    }
}