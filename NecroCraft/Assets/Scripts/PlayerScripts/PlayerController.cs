using TroopScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public Vector3 Position { get; private set; }
        public Vector3 MovementDirection { get; private set; }
        public Vector3 LookingDirection { get; private set; }
        
        public event Action<float> OnHealthChanged;
        public event Action<float> OnXpChanged;

        [SerializeField] public float speed = 2;
        [SerializeField] public float maxHp;
        [SerializeField] public float damageTimer = 0.1f;
        [SerializeField] public int xp = 1;

        private Animator _animator;
        private float _currentHp;
        private float _movementX;
        private float _movementY;
        private float _lastDamageTime;
        private int _horizontal = 1;
        
        private float _speedUpgrade;
        private float _maxHpUpgrade;

        private void Awake()
        {
            // Ensure that only one instance of this class exists
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }


        void Start()
        {
            LookingDirection = new Vector3(1, 0, 0);
            _currentHp = maxHp;
            _animator = GetComponentInChildren<Animator>();

            _maxHpUpgrade = maxHp * 0.05f;
            _speedUpgrade = speed * 0.05f;
        }

        private void Update() {
            switch (_movementX)
            {
                case > 0:
                    _horizontal = 1;
                    break;
                case < 0:
                    _horizontal = -1;
                    break;
            }

            _animator.SetInteger("isMoving", Mathf.Abs(_movementX) > 0 || Mathf.Abs(_movementY) > 0 ? 1 : 0);
            transform.localScale = new Vector3(_horizontal, 1, 1);
        }
        // Start is called before the first frame update
        void FixedUpdate()
        {
            Vector2 movement = new Vector2(_movementX, _movementY);
            transform.Translate(movement * (speed * Time.deltaTime));
            Position = transform.position;
        }

        void OnMove(InputValue value)
        {
            Vector2 v = value.Get<Vector2>();
            MovementDirection = v;
            if (v != Vector2.zero)
                LookingDirection = v;

            _movementX = v.x;
            _movementY = v.y;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Collectibles")) return;
            xp++;
            OnXpChanged?.Invoke(xp/10f);
            Destroy(other.gameObject, 0f);
            if(xp == 10){
                xp = 0;
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
                OnHealthChanged?.Invoke(_currentHp/maxHp);
            }
        }
        
        public float GetCurrentHp()
        {
            return _currentHp;
        }

        public void RecoverHealth(float amountPct)
        {
            float lostHp = maxHp - _currentHp;
            _currentHp = Mathf.Min(_currentHp + lostHp * amountPct, maxHp);
            OnHealthChanged?.Invoke(_currentHp/maxHp);
        }

        public void UpgradeMaxHealth()
        {
            float currentHpPct = _currentHp / maxHp;
            maxHp += _maxHpUpgrade;
            _currentHp = Mathf.Min(currentHpPct * maxHp, maxHp);
            OnHealthChanged?.Invoke(_currentHp/maxHp);
        }

        public void UpgradeSpeed()
        {
            speed += _speedUpgrade;
        }
    }
}