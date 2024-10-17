using Events;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVC
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Unit _player;
        private IEventsContainer _eventsContainer;

        [Inject]
        public void Construct(IEventsContainer eventsContainer)
        {
            _eventsContainer = eventsContainer;
            _eventsContainer?.OnTakeDamage.AddListener(OnTakeDamage);
        }

        private void Start()
        {
            UpdateView();
        }

        private void OnTakeDamage(Unit unit)
        {
            if (unit == _player)
            {
                UpdateView();
            }
        }

        public void UpdateView()
        {
            if (_player.Model == null)
                return;
            if (_healthSlider != null && _player.Model.MaxHealth != 0)
            {
                _healthSlider.value = (float)_player.Model.Health / (float)
               _player.Model.MaxHealth;
            }
        }

        private void OnDestroy()
        {
            _eventsContainer?.OnTakeDamage.RemoveListener(OnTakeDamage);
        }
    }

}