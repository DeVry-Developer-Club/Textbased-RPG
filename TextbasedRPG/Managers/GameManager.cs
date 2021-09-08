using TextbasedRPG.Core;
using TextbasedRPG.Core.Actors;
using TextbasedRPG.Core.Actors.Configs;
using TextbasedRPG.Core.Areas;
using TextbasedRPG.Core.Items;

namespace TextbasedRPG.Managers
{
    /// <summary>
    /// Handles the overall gameplay loop for our project
    /// </summary>
    public class GameManager
    {
        private Player player = new Player(new ActorConfig
        {
            Name = "Legolas",
            Description = "Super awesome arrow elf",
            MaxHitpoints = 120,
            StartingHitpoints = 120,
            StartingCurrency = 5
        });
        
        private Area currentArea = new Market()
        {
            Name = "Me Castle",
            Inventory = new Inventory(Util.LoadFromFile<Item>())
        };

        /// <summary>
        /// Enter starting area and begin core game loop
        /// </summary>
        public void GameLoop()
        {
            currentArea.Enter(ref player);
            
            while (!currentArea.IsPlayerLeaving)
            {
                currentArea.GameLoop();
            }
        }
    }
}