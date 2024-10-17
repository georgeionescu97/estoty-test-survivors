using UnityEngine;
using Zenject;

namespace Units
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _moveSpeed = 2;
        [SerializeField] private float _stopingDistance = 0.5f;

        private Vector2 _moveDirection = Vector2.zero;
        private IPlayerController _playerController;

        [Inject]
        public void Construct(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Start()
        {
            _rb.gravityScale = 0;
        }

        private void Update()
        {
            if (_playerController == null)
            {
                Debug.Log($"{_playerController} is null");
                return;
            }

            _moveDirection = (_playerController.PlayerTransform.position - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            if (_rb == null) return;

            var distance = Vector2.Distance(_playerController.PlayerTransform.position, transform.position);

            if (distance < _stopingDistance)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            _rb.velocity = _moveDirection * _moveSpeed;

            if (_moveDirection.x != 0)
            {
                float angle = _moveDirection.x > 0 ? 180 : 0;

                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
    }
}

