using UnityEngine;

namespace Units
{
    public class Collectable : Unit
    {
        [SerializeField] private int _healAmmount = 3;

        private void Awake()
        {
            _eventsContainer?.OnUnitDied.AddListener(UnitDied);
        }

        private void UnitDied(Unit unit)
        {
            if (unit == this)
            {
                Destroy(gameObject);  // Here i should implement same ObjectPooling principle with a spawner but i didn't had time
                                      // I just want to point out that i can scale the current Unit implementation
            }
        }

        protected override void OnUnitTrigger(Unit unit)
        {
            base.OnUnitTrigger(unit);
            if (unit is IPlayerController)
            {
                unit.Model.Heal(_healAmmount);
            }
        }

        private void OnDestroy()
        {
            _eventsContainer?.OnUnitDied.RemoveListener(UnitDied);
        }
    }
}

