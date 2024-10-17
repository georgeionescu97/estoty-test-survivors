
namespace MVC
{
    public class EnemyKillsCount : IEnemyKillsCount
    {    
        public int EnemyKilled { get; private set; }

        private const int _maxEnemiesPerLevel = 10;
        public int MaxEnemiesPerLevel => _maxEnemiesPerLevel;

        public void AddEnemyKilled()
        {
            EnemyKilled++;
        }

        public void Restore()
        {
            EnemyKilled = 0;
        }
    }
}
