using UnityEngine;

namespace CameraFollow
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float smoothSpeed = 0.125f;

        void LateUpdate()
        {
            Vector3 desiredPosition = _player.position;
            desiredPosition.z = transform.position.z;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
