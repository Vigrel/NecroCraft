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

        private float _currentHp;
        private float _movementX;
        private float _movementY;
        private float _lastDamageTime;

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

            if (_currentHp <= 0)
            {
                Time.timeScale = 0;
            }
        }
    }
}