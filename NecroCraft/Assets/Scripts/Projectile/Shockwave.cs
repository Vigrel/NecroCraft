using System;
using UnityEngine;
using PlayerScripts;

namespace Projectile
{
    public class Shockwave : MonoBehaviour
    {
        [SerializeField] public float speed = 2f;
        [SerializeField] public float maxRadius = 5f;
        [SerializeField] public float cooldown = 5f;
        
        private CircleCollider2D _circleCollider;
        private float _currentRadius;
        private float _lastShockwaveTime;

        private void Start()
        {
            _currentRadius = 0;
            _lastShockwaveTime = Time.fixedTime+cooldown;
            _circleCollider = GetComponent<CircleCollider2D>();
        }
        
        private void Update()
        {
            float elapsedTime = Time.fixedTime - _lastShockwaveTime;
            if (elapsedTime < cooldown) return;
            if (_currentRadius >= maxRadius)
            {
                _circleCollider.radius = 0;
                _currentRadius = 0;
                _lastShockwaveTime = Time.fixedTime;
                return;
            }
            
            transform.position = PlayerController.Instance.Position;
            _currentRadius += speed * Time.deltaTime;
            _circleCollider.radius = _currentRadius;
            
        }

    }
}
