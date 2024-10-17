using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Spawners
{
    public class BulletSpawner : BaseSpawner
    {
        [SerializeField] private Transform _spawnPoint;

        protected override Unit InstantiatePrefab()
        {
            return Instantiate(_prefab);
        }

        public override void Spawn()
        {
            var bullet = _pool.Get();
            bullet.transform.position = _spawnPoint.position;
        }

        protected override void UnitDied(Unit unit)
        {
            if (unit is IBullet)
            {
                _pool.Release(unit);
            }
        }
    }
}

