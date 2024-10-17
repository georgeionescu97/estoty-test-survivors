using Events;
using MVC;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected int _baseDamage = 1;
        public int BaseDamage => _baseDamage;
        [SerializeField, FormerlySerializedAs("_health")] private int _maxHealth = 1;
        protected UnitModel _model;
        public UnitModel Model => _model;
        private IUnitsContainer _unitsContainer;
        protected IEventsContainer _eventsContainer;

        [Inject]
        public void Construct(IUnitsContainer unitsContainer, IEventsContainer eventsContainer)
        {
            _unitsContainer = unitsContainer;
            _eventsContainer = eventsContainer;
            _unitsContainer?.RegisterUnit(this);
        }

        protected virtual void OnEnable()
        {
            InitState();
            if (_unitsContainer != null)
            {
                _unitsContainer?.RegisterUnit(this);
            }
        }

        protected virtual void InitState()
        {
            _model = new UnitModel(_maxHealth);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Unit unit))
            {
                OnUnitTrigger(unit);
            }
        }

        protected virtual void OnUnitTrigger(Unit unit)
        {
            OnTakeDamage(unit);
        }

        public virtual void OnTakeDamage(Unit attacker)
        {
            _model.Injure(attacker.BaseDamage);
            if (!_model.IsAlive)
            {
                OnUnitDied();
            }
            _eventsContainer?.OnTakeDamage.Invoke(this);
        }

        protected virtual void OnUnitDied()
        {
            _eventsContainer?.OnUnitDied?.Invoke(this);
        }

        protected virtual void OnDisable()
        {
            _unitsContainer?.RemoveUnit(this);
        }
    }
}

