using Events;
using Spawners;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Units
{
    public class PlayerAttack : MonoBehaviour, IPlayerAttack
    {
        [SerializeField, Tooltip("The radius within which the player can attack targets.")] private float _attackRadius = 2f;
        [SerializeField, Tooltip("Time in seconds since the last fire action.")] private float _attackTime = 2f;
        [SerializeField] private BaseSpawner _bulletSpawner;
        [SerializeField] private Transform _spawnPoint;

        private Transform _target = null;
        public Transform Target => _target;
        public Vector2 TargetPosition => _target.position;

        private float lastFireTime;

        private IUnitsContainer _unitsContainer;
        private IEventsContainer _eventsContainer;

        [Inject]
        public void Construct(IUnitsContainer unitsContainer, IEventsContainer eventsContainer)
        {
            _unitsContainer = unitsContainer;
            _eventsContainer = eventsContainer;
            _eventsContainer.OnUnitDied.AddListener(OnUnitDied);
        }

        private void OnUnitDied(Unit unit)
        {
            if(unit.transform == _target)
            {
                _target = null;               
            }
        }

        private void OnEnable()
        {
            lastFireTime = Time.time;
        }

        void Update()
        {
            Fire();
        }

        private async void Fire()
        {
            if (!CanFire()) return;

            if (!CanSetTarget()) return;
            _bulletSpawner.Spawn();
        }

        private bool CanFire()
        {
            if (Time.time - lastFireTime >= _attackTime)
            {
                lastFireTime = Time.time;
                return true;
            }
            return false;
        }

        private bool CanSetTarget()
        {
            var enemyTargets = _unitsContainer.GetUnits<EnemyController>().Where(e =>
            Vector2.Distance(e.transform.position, this.transform.position) < _attackRadius); // decouple this

            if (enemyTargets == null || enemyTargets.Count() == 0)
            {
                return false;
            }

            FindClosestEnemy(enemyTargets);
            return true;
        }

        public Transform FindClosestEnemy(IEnumerable<Unit> enemies)
        {
            _target = null;
            float closestDistance = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _target = enemy.transform;
                }
            }

            return _target;
        }

        public Vector2 MovementDirection()
        {
            return (_target.position - _spawnPoint.position).normalized;
        }
    }
}

