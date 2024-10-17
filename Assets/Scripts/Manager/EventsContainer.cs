using Units;
using UnityEngine.Events;

namespace Events
{
    public class EventsContainer : IEventsContainer
    {
        public UnityEvent OnGameStarted { get; private set; } = new UnityEvent();
        public UnityEvent<bool> OnGameEnded { get; private set; } = new UnityEvent<bool>();
        public UnityEvent<Unit> OnUnitDied { get; private set; } = new UnityEvent<Unit>();
        public UnityEvent<Unit> OnTakeDamage { get; private set; } = new UnityEvent<Unit>();
        public UnityEvent OnChangedEnemyKilled { get; private set; } = new UnityEvent();
        public UnityEvent OnLevelUp { get; private set; } = new UnityEvent();
    }

}
