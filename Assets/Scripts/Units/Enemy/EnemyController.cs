using System.Threading;
using System.Threading.Tasks;
using Units;
using UnityEngine;

namespace Units
{
    public class EnemyController : Unit, IEnemyController
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isAtTrigger = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override void OnUnitTrigger(Unit unit)
        {
            base.OnUnitTrigger(unit);
            if (unit is IPlayerController)
            {
                StartDamageOverTime(unit);
            }
        }

        private async void StartDamageOverTime(Unit unit)
        {
            _isAtTrigger = true;
            while (_isAtTrigger)
            {
                await Task.Delay(3000);
                if (_cancellationTokenSource.IsCancellationRequested) return;
                unit.OnTakeDamage(this);
                Debug.Log($"{unit.Model.Health}");

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _isAtTrigger = false;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            CancelTask();
        }

        private void CancelTask()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource?.Cancel();
            }
            _cancellationTokenSource.Dispose();
        }

        private void OnDestroy()
        {
            CancelTask();
        }
    }
}

