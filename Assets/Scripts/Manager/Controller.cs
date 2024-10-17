using Events;
using System;
using Units;
using Zenject;

namespace MVC
{
    public class Controller : IController, IDisposable
    {
        private IEventsContainer _eventsContainer;
        private IEnemyKillsCount _enemyKillsCount;
        private ILevels _levels;

        [Inject]
        public void Construct(IEventsContainer eventsContainer, IEnemyKillsCount enemyKillsCount, ILevels levels)
        {
            _eventsContainer = eventsContainer;
            _enemyKillsCount = enemyKillsCount;
            _levels = levels;
            _eventsContainer?.OnUnitDied.AddListener(OnUnitDied);
        }

        private void OnUnitDied(Unit unit)
        {
            if (unit is IEnemyController)
            {
                _enemyKillsCount.AddEnemyKilled();

                if (_enemyKillsCount.EnemyKilled == _enemyKillsCount.MaxEnemiesPerLevel)
                {
                    _levels.LevelUp();
                    _enemyKillsCount.Restore();
                    _eventsContainer.OnLevelUp.Invoke();
                }
                _eventsContainer.OnChangedEnemyKilled.Invoke();
            }
        }

        public void Dispose()
        {
            _eventsContainer?.OnUnitDied.RemoveListener(OnUnitDied);
        }
    }
}

