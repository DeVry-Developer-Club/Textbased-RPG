using TextbasedRPG.Managers;

namespace TextbasedRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();
            
            manager.GameLoop();
        }
    }
}