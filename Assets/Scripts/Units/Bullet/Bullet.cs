using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Units
{
    public class Bullet : Unit, IBullet
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _moveSpeed = 4;
        private IPlayerAttack _playerAttack;
        private Vector2 _targetPosition = Vector2.zero;
        protected CancellationTokenSource _cancellationTokenSource;

        [Inject]
        public void Construct(IPlayerAttack playerAttack)
        {
            _playerAttack = playerAttack;
            _targetPosition = _playerAttack.MovementDirection();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _rb.velocity = Vector2.zero;
            if (_playerAttack != null)
            {
                _targetPosition = _playerAttack.MovementDirection();            
            }
            _cancellationTokenSource = new CancellationTokenSource();
            ReleaseTime();
        }

        private void Start()
        {
            _rb.gravityScale = 0;
        }

        void FixedUpdate()
        {
            if (_rb == null) return;
            _rb.velocity = _targetPosition * _moveSpeed;
        }

        private async void ReleaseTime()
        {
            await Task.Delay(4000);
            if (_cancellationTokenSource.IsCancellationRequested) return;
            _eventsContainer.OnUnitDied.Invoke(this);
        }

        public override void OnTakeDamage(Unit attacker)
        {
            _eventsContainer.OnUnitDied?.Invoke(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            CancelTask();
            _rb.velocity = Vector2.zero;
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
