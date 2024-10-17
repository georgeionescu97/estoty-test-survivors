using Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVC
{
    public class LevelsUI : MonoBehaviour
    {
        [SerializeField] private Slider _levelSlider;
        [SerializeField] private Text _textLevel;

        private ILevels _levels;
        private IEventsContainer _eventsContainer;
        private IEnemyKillsCount _enemyKillsCount;

        [Inject]
        public void Construct(ILevels levels, IEventsContainer eventContainer, IEnemyKillsCount enemyKillsCount)
        {
            _levels = levels;
            _eventsContainer = eventContainer;
            _enemyKillsCount = enemyKillsCount;
            _eventsContainer.OnLevelUp.AddListener(OnLevelUp);
            _eventsContainer.OnChangedEnemyKilled.AddListener(OnChangedEnemyKilled);
        }

        private void Start()
        {
            UpdateViewLevel();
            UpdateLevelSlider();
        }

        private void OnLevelUp()
        {
            UpdateViewLevel();
        }

        private void OnChangedEnemyKilled()
        {
            UpdateLevelSlider();
        }

        public void UpdateViewLevel()
        {
            _textLevel.text = $"Lv. {_levels.Level}";
            UpdateLevelSlider();
        }

        private void UpdateLevelSlider()
        {
            if (_levels == null)
                return;
            if (_levelSlider != null)
            {
                _levelSlider.value = (float)_enemyKillsCount.EnemyKilled /
              _enemyKillsCount.MaxEnemiesPerLevel;
            }
        }

        private void OnDestroy()
        {
            _eventsContainer?.OnLevelUp.RemoveListener(OnLevelUp);
            _eventsContainer?.OnChangedEnemyKilled.RemoveListener(OnChangedEnemyKilled);
        }
    }

}
