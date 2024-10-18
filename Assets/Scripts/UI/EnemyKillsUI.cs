using Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVC
{
    public class EnemyKillsUI : MonoBehaviour
    {
        [SerializeField] Text _numberOfEnemyKilled;
        private IEnemyKillsCount _enemyKillsCount;
        private IEventsContainer _eventsContainer;

        [Inject]
        public void Construct(IEnemyKillsCount enemyKillsCount, IEventsContainer eventContainer)
        {
            _enemyKillsCount = enemyKillsCount;
            _eventsContainer = eventContainer;
            _eventsContainer.OnChangedEnemyKilled.AddListener(OnChangedEnemyKilled);
        }

        private void OnChangedEnemyKilled()
        {
            UpdateView();
        }

        private void Start()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            if (_enemyKillsCount == null)
                return;
            _numberOfEnemyKilled.text = $"{_enemyKillsCount.EnemyKilled}";
        }

        private void OnDestroy()
        {
            _eventsContainer?.OnChangedEnemyKilled.RemoveListener(OnChangedEnemyKilled);
        }
    }

}
