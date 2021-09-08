namespace TextbasedRPG.Core.Items
{
    public class Item
    {
        public string Name { get; init; } = "Default Name";
        public float Weight { get; init; } = 0.0f;
        public string Description { get; init; } = "";
        
        /// <summary>
        /// Is <see cref="StackSize"/> > 1
        /// </summary>
        public bool IsStackable => StackSize > 1;
        
        /// <summary>
        /// Current number of items in stack
        /// </summary>
        public int ItemCount { get; set; } = 1;
        
        /// <summary>
        /// Maximum amount of items allowed on this stack
        /// </summary>
        public int StackSize { get; init; } = 1;
        
        /// <summary>
        /// Cost of item
        /// </summary>
        public float Price { get; init; } = 0.01f;
        
        /// <summary>
        /// Has this item reached <see cref="StackSize"/>
        /// </summary>
        public bool IsMaxed => ItemCount == StackSize;
        
        public Item()
        {
            ItemCount = StackSize;
        }

        /// <summary>
        /// Increases stack by <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Increase <see cref="ItemCount"/> by this amount</param>
        public void IncreaseStack(int amount = 1)
        {
            ItemCount += amount;

            if (ItemCount > StackSize)
                ItemCount = StackSize;
        }

        /// <summary>
        /// Remove <paramref name="amount"/> from stack
        /// </summary>
        /// <param name="amount">Decreases <see cref="ItemCount"/> by this amount</param>
        public void DecreaseStack(int amount = 1)
        {
            ItemCount -= amount;

            if (ItemCount < 0)
                ItemCount = 0;
        }
    }
}