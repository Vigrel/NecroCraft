using UnityEngine;
using PlayerScripts;

namespace Projectile
{
    public class Guitar : MonoBehaviour
    {
        [SerializeField] public float speed = 1f;
        [SerializeField] public float rotationSpeed = 200f;

        private Camera _mainCamera;
        private Vector3 _direction;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _mainCamera = Camera.main;
            _direction = PlayerController.Instance.LookingDirection;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!IsVisibleOnScreen())
                Destroy(gameObject);

            transform.Translate(_direction * (speed * Time.deltaTime), Space.World);

            transform.Rotate(
                new Vector3(0, 0, +_direction.x < 0 ? 1 : -1),
                rotationSpeed * Time.deltaTime);
        }

        private bool IsVisibleOnScreen()
        {
            // Get the object's position in screen coordinates
            Vector3 screenPosition = _mainCamera.WorldToScreenPoint(transform.position);

            // Check if the object is within the camera's view
            return screenPosition.x > 0 && screenPosition.x < Screen.width &&
                   screenPosition.y > 0 && screenPosition.y < Screen.height &&
                   screenPosition.z > 0;
        }
    }
}