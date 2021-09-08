using TextbasedRPG.Core.Items;

namespace TextbasedRPG.Core.Actors.Configs
{
    /// <summary>
    /// Basic config required for any <see cref="Actor"/> instance
    /// </summary>
    public class ActorConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double StartingHitpoints { get; set; }
        public double MaxHitpoints { get; set; }
        public Item StartingWeapon { get; set; }
        public float StartingCurrency { get; set; }
        public Inventory StartingInventory { get; set; }
    }
}