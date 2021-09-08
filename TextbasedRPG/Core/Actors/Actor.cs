using System;
using System.Collections.Generic;
using TextbasedRPG.Core.Actors.Configs;
using TextbasedRPG.Core.Items;

namespace TextbasedRPG.Core.Actors
{
    /// <summary>
    /// Basic implementation of what characters are in our game world
    /// </summary>
    public class Actor
    {
        public string Name { get; init; }
        public string Description { get; init; }
        
        /// <summary>
        /// Current health
        /// </summary>
        public double Hitpoints { get; protected set; }
        
        /// <summary>
        /// Maximum amount of health this instance can have
        /// </summary>
        public double MaxHitpoints { get; init; }
        
        /// <summary>
        /// Amount of currency this actor has on hand
        /// </summary>
        public float Coins { get; protected set; }
        
        /// <summary>
        /// Primary weapon (if any) equipped by this instance
        /// </summary>
        public Item PrimaryWeapon { get; set; }
        
        /// <summary>
        /// Items this instance has on their person
        /// </summary>
        public Inventory Inventory { get; set; }

        public Actor(ActorConfig config)
        {
            Hitpoints = config.StartingHitpoints;
            MaxHitpoints = config.MaxHitpoints;
            Name = config.Name;
            Description = config.Description;
            PrimaryWeapon = config.StartingWeapon;
            Coins = config.StartingCurrency;
            Inventory = config.StartingInventory ?? new Inventory(new List<Item>());
        }
        
        /// <summary>
        /// Displays table of items in Actor's inventory
        /// </summary>
        public void DisplayInventory()
        {
            Console.WriteLine($"-----{Name}'s Inventory-----");
            Console.WriteLine(Inventory);
        }
        
        /// <summary>
        /// Add <paramref name="amount"/> of health to instance
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(double amount)
        {
            Hitpoints += amount;

            if (Hitpoints > MaxHitpoints)
                Hitpoints = MaxHitpoints;
        }
        
        /// <summary>
        /// Apply <paramref name="amount"/> of damage to instance
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(double amount)
        {
            Hitpoints += amount;

            if (Hitpoints < 0)
                Hitpoints = 0;
        }
        
        public override string ToString()
        {
            return $"Name: {Name}\n" +
                   $"Desc: {Description}\n" +
                   $"Hitpoints: {Hitpoints} / {MaxHitpoints}\n";
        }

        /// <summary>
        /// Add <paramref name="amount"/> to <see cref="Coins"/>
        /// </summary>
        /// <param name="amount"></param>
        public void AddCoins(float amount)
        {
            Coins += amount;
        }

        /// <summary>
        /// Subtract <paramref name="amount"/> from <see cref="Coins"/>
        /// </summary>
        /// <param name="amount"></param>
        public void SubtractCoins(float amount)
        {
            Coins -= amount;
        }
    }
}