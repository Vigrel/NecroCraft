using UnityEngine;
using PlayerScripts;

namespace TroopScripts
{
    public class Ally : MonoBehaviour
    {
        [SerializeField] public float moveSpeed = 1f;
        [SerializeField] public float maxHp = 5f;
        [SerializeField] public float damageTimer = 0.1f;
        [SerializeField] public float circleRadius = 2f;
        [SerializeField] public float circleOffset = 1f;
        [SerializeField] private float detectionRadius = 3f;
        [SerializeField] private LayerMask enemyLayerMask;

        private Vector3 _initialPositionFromPlayer;
        private float _currentHp;
        private float _lastDamageTime;
        private Animator _animator;
        private float _randAngle;
        private Transform _targetEnemy;

        void Start()
        {
            _randAngle = Random.Range(0f, 1f);
            _initialPositionFromPlayer = PlayerController.Instance.Position - transform.position;
            _currentHp = maxHp;
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Vector3 currentPlayerPosition = PlayerController.Instance.Position;
            float angle = Time.time * moveSpeed * _randAngle;
            Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * circleRadius;

            if (_targetEnemy != null)
            {
                Vector3 targetPosition = _targetEnemy.position;
                transform.position = Vector3.MoveTowards(
                    transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else{
                Vector3 targetPosition = currentPlayerPosition - _initialPositionFromPlayer + offset + Vector3.up * circleOffset ;
                transform.position = Vector3.MoveTowards(
                    transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            

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

            // Check for nearby enemies
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayerMask);
            if (colliders.Length > 0)
            {
                // Find the closest enemy
                float closestDistance = Mathf.Infinity;
                Transform closestEnemy = null;

                foreach (Collider2D collider in colliders)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = collider.transform;
                    }
                }

                // Set the target enemy
                _targetEnemy = closestEnemy;
            }
            else
            {
                // Reset the target enemy
                _targetEnemy = null;
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
                TroopDamage.DecrementAllyCount();
            }
        }
    }
}