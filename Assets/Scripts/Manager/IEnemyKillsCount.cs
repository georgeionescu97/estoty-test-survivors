
namespace MVC
{
    public interface IEnemyKillsCount
    {
        int EnemyKilled { get; }
        int MaxEnemiesPerLevel { get; }
        void AddEnemyKilled();
        void Restore();
    }
}
