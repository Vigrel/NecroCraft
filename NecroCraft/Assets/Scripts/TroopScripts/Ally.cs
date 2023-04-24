using UnityEngine;
using PlayerScripts;

namespace TroopScripts
{
    public class Ally : MonoBehaviour
    {
        [SerializeField] public float moveSpeed = 1f;
        [SerializeField] public float maxHp = 5f;
        [SerializeField] public float damageTimer = 0.1f;

        private Vector3 _initialDistanceFromPlayer;
        private Vector3 _lastPlayerPosition;
        private float _currentHp;
        private float _lastDamageTime;

        void Start()
        {
            _initialDistanceFromPlayer = PlayerController.Instance.Position - transform.position;
            _currentHp = maxHp;
        }

        private void Update()
        {
            Vector3 currentPlayerPosition = PlayerController.Instance.Position;

            Vector3 targetPosition = currentPlayerPosition - _initialDistanceFromPlayer;
            transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, moveSpeed * Time.deltaTime);

            
            var playerHorizontalDirection = PlayerController.Instance.MovementDirection.x;
            var currLocalScale = transform.localScale;
            
            switch (playerHorizontalDirection)
            {
                case > 0 when currLocalScale.x < 0:
                    transform.localScale = new Vector3(1, 1, 1);
                    break;
                case < 0 when currLocalScale.x > 0:
                    transform.localScale = new Vector3(-1, 1, 1);
                    break;
            }

        }

        void OnCollisionStay2D(Collision2D other)
        {
            float damageElapsedTime = Time.fixedTime - _lastDamageTime;
            if (damageElapsedTime < damageTimer) return;

            float damageToTake = TroopDamage.GetDamageForEnemy(other.gameObject.tag);

            if (damageToTake != 0)
            {
                _currentHp -= damageToTake;
                _lastDamageTime = Time.fixedTime;
            }

            if (_currentHp <= 0)
            {
                Destroy(gameObject, 0f);
            }
        }
    }
}