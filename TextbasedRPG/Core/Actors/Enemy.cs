using TextbasedRPG.Core.Actors.Configs;

namespace TextbasedRPG.Core.Actors
{
    /// <summary>
    /// Basic implementation of an enemy in our game world
    /// </summary>
    public class Enemy : Actor
    {
        public int Age { get; init; }

        public Enemy(EnemyConfig config) : base(config)
        {
            Age = config.Age;
        }

        public override string ToString()
        {
            return base.ToString() + $"Age: {Age}\n";
        }
    }
}