using UnityEngine;

namespace Units
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private PlayerAttack _playerAttack;
        private Vector2 _moveDirection = Vector2.zero;


        private void Start()
        {
            _rb.gravityScale = 0;
        }

        void Update()
        {
            _moveDirection = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical")).normalized;

            if (_playerAttack.Target == null) return;
            float angle = _playerAttack.TargetPosition.x > transform.position.x ? 0 : 180;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        private void FixedUpdate()
        {
            if (_rb == null) return;
            _rb.velocity = _moveDirection * _moveSpeed;
        }
    }
}

