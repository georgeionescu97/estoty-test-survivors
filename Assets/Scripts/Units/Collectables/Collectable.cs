using UnityEngine;

namespace Units
{
    public class Collectable : Unit, ICollectables
    {
        [SerializeField] private int _healAmmount = 3;

        protected override void OnUnitTrigger(Unit unit)
        {
            base.OnUnitTrigger(unit);
            if (unit is IPlayerController)
            {
                unit.Model.Heal(_healAmmount);
            }
        }
    }
}

