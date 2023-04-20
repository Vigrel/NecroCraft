using UnityEngine;
using PlayerScripts;

namespace Projectile
{
    public class Note : MonoBehaviour
    {
        [SerializeField] public float speed = 1f;
         
        private Camera _mainCamera;
        private Vector3 _direction;

        private void Start()
        {
            _mainCamera = Camera.main;
            _direction = PlayerController.Instance.LookingDirection;
        }

        private void Update()
        {
            if (!IsVisibleOnScreen())
                Destroy(gameObject);

            transform.Translate(_direction * (speed * Time.deltaTime));
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