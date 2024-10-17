using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : BaseSpawner
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float spawnDistance = 5f;
        [SerializeField] private Unit _enemyPrefab2;

        [SerializeField, Tooltip("Time in miliseconds(for 1 second type 1000).")] private int _timeBetweenSpawnsInMiliseconds = 2000;

        protected override void Start()
        {
            base.Start();
            StartSpawning();
        }

        protected override Unit InstantiatePrefab()
        {
            Unit prefabToInstantiate = Random.value > 0.5f ? _prefab : _enemyPrefab2;
            return Instantiate(prefabToInstantiate);
        }

        protected override void UnitDied(Unit unit)
        {
            if(unit is IEnemyController)
            {
                _pool.Release(unit);
            }
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

        public override void Spawn()
        {
            float spawnX = Random.value > 0.5f ? Random.Range(-0.5f, -1.1f) : Random.Range(1.1f, 1.5f);

            float spawnY = Random.Range(-1f, 1f);
            Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(spawnX, spawnY, spawnDistance));

            var enemy = _pool.Get();
            enemy.transform.position = new Vector2(spawnPosition.x, spawnPosition.y); // Remake this because sometimes it spawn inside camera view
        }
    }
}
