
using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Spawners
{
    public class CollectablesSpawner : BaseSpawner
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField, Tooltip("Time in miliseconds(for 1 second type 1000).")] private int _timeBetweenSpawnsInMiliseconds = 5000;
        private float spawnDistance = 5f;

        protected override void Start()
        {
            base.Start();
            StartSpawning();
        }

        private async void StartSpawning()
        {
            _canSpawn = true;
            while (_canSpawn)
            {
                if (_cancellationTokenSource.IsCancellationRequested) return;
                Spawn();
                await Task.Delay(_timeBetweenSpawnsInMiliseconds);
            }
        }

        protected override Unit InstantiatePrefab()
        {
            return Instantiate(_prefab);
        }

        public override void Spawn()
        {

            float spawnX = Random.value > 0.5f ? Random.Range(0, -0.5f) : Random.Range(0f, 0.5f);

            float spawnY = Random.Range(-1f, 1f);
            Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(spawnX, spawnY, spawnDistance));

            var collect = _pool.Get();
            collect.transform.position = new Vector2(spawnPosition.x, spawnPosition.y);
        }

        protected override void UnitDied(Unit unit)
        {
            if (unit is ICollectables)
            {
                _pool.Release(unit);
            }
        }
    }

}
