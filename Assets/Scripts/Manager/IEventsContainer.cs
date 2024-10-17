using Units;
using UnityEngine.Events;

namespace Events
{
    public interface IEventsContainer
    {
        UnityEvent OnGameStarted { get; }
        UnityEvent<bool> OnGameEnded { get; }
        UnityEvent<Unit> OnUnitDied { get; }
        UnityEvent<Unit> OnTakeDamage { get; }
        UnityEvent OnChangedEnemyKilled { get; }
        UnityEvent OnLevelUp { get; }
    }

}
