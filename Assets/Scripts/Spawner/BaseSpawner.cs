using Events;
using System.Threading;
using Units;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Spawners
{
    public abstract class BaseSpawner : MonoBehaviour
    {
        [SerializeField] protected Unit _prefab;
        protected ObjectPool<Unit> _pool;
        protected int _initSize = 20;
        protected bool _canSpawn = false;
        protected CancellationTokenSource _cancellationTokenSource;
        private IEventsContainer _eventsContainer;

        [Inject]
        public void Construct(IEventsContainer eventsContainer)
        {
            _eventsContainer = eventsContainer;
            eventsContainer.OnUnitDied.AddListener(OnUnitDied);
        }

        private void Awake()
        {
            _pool = new ObjectPool<Unit>(() =>
            {
                return InstantiatePrefab();
            },
             enemy => { enemy.gameObject.SetActive(true); },
             enemy => { enemy.gameObject.SetActive(false); },
             enemy => { Destroy(enemy.gameObject); },
             false, _initSize, 1000);
        }

        protected virtual void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected abstract Unit InstantiatePrefab();

        public abstract void Spawn();

        protected abstract void UnitDied(Unit unit);

        private void OnUnitDied(Unit unit)
        {
            UnitDied(unit);
        }

        private void OnDestroy()
        {
            _eventsContainer.OnUnitDied.RemoveListener(OnUnitDied);
            _canSpawn = false;
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource?.Cancel();
            }
            _cancellationTokenSource.Dispose();
        }
    }
}


