using System;
using TextbasedRPG.Core.Actors;
using TextbasedRPG.Managers;

namespace TextbasedRPG.Core
{
    /// <summary>
    /// Represents locations a <see cref="Player"/> can navigate to
    /// </summary>
    public abstract class Area
    {
        /// <summary>
        /// Display name of area
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// (if any) -- area the player just came from
        /// </summary>
        public Area PreviousArea { get; set; }
        
        /// <summary>
        /// Reference to player within this location
        /// </summary>
        protected Player Player;
        
        /// <summary>
        /// Is the player currently trying to leave?
        /// </summary>
        /// <remarks>
        /// This property is utilized by the <see cref="GameManager"/>
        /// </remarks>
        public bool IsPlayerLeaving { get; protected set; } = false;

        /// <summary>
        /// Behaves like a constructor where you can greet and initialize whatever is needed
        /// for this area
        /// </summary>
        /// <param name="player">Reference to player</param>
        /// <param name="previous">Area the player can go back to</param>
        public virtual void Enter(ref Player player, Area previous = null)
        {
            Player = player;
            PreviousArea = previous;
            IsPlayerLeaving = false;
            
            Console.WriteLine(player != null 
                ? $"{player.Name} is entering {Name}" 
                : $"Entering {Name}...");
        }

        /// <summary>
        /// Core mechanics of what happens in this area
        /// </summary>
        public abstract void GameLoop();

        /// <summary>
        /// Code that is invoked while the player is exiting.
        /// </summary>
        /// <remarks>
        /// Can do whatever necessary cleanup here, even add a goodbye message if you want
        /// </remarks>
        public virtual void Exit()
        {
            IsPlayerLeaving = true;
            Console.WriteLine(Player != null 
                ? $"{Player.Name} is leaving {Name}" 
                : $"Leaving {Name}...");
        }
    }
}