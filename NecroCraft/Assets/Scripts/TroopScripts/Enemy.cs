﻿using System;
using UnityEngine;
using PlayerScripts;

namespace TroopScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public float moveSpeed = 1;
        [SerializeField] public float maxHp = 5;
        [SerializeField] public float maxDistance = 25f;
        [SerializeField] public float damageTimer = 0.1f;
        [SerializeField] public GameObject xpPrefab;

        private Animator _animator;
        private float _currentHp;
        private float _lastDamageTime;

        private void Start()
        {
            _currentHp = maxHp;
            _animator = GetComponentInChildren<Animator>();
        }

        protected virtual void Update()
        {
            var playerPos = PlayerController.Instance.Position;
            var selfPos = transform.position;

            selfPos = RepositionWhenFarAway(selfPos, playerPos);
            MoveTowardsPlayer(selfPos, playerPos);

            if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private void MoveTowardsPlayer(Vector3 selfPos, Vector3 playerPos)
        {
            transform.position = Vector3.MoveTowards(
                selfPos, playerPos, moveSpeed * Time.deltaTime);
        }

        private Vector3 RepositionWhenFarAway(Vector3 selfPos, Vector3 playerPos)
        {
            var distanceToPlayer = Vector3.Distance(selfPos, playerPos);
            if (distanceToPlayer > maxDistance)
            {
                var newPos = playerPos + PlayerController.Instance.MovementDirection * maxDistance / 1.2f;
                transform.position = newPos;
                return newPos;
            }

            return selfPos;
        }

        void OnCollisionStay2D(Collision2D other)
        {
            float damageElapsedTime = Time.fixedTime - _lastDamageTime;
            if (damageElapsedTime < damageTimer) return;

            if (other.gameObject.CompareTag("Player")) return;

            float damageToTake = TroopDamage.GetDamageForAlly(other.gameObject.tag);

            if (damageToTake != 0)
            {
                _currentHp -= damageToTake;
                _lastDamageTime = Time.fixedTime;
            }

            if (_currentHp <= 0)
            {
                Destroy(gameObject, 0f);
                TroopDamage.DecrementEnemyCount();
                Instantiate(
                    xpPrefab, gameObject.transform.position, Quaternion.identity
                );
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Guitar")) return;

            float damageToTake = TroopDamage.GetDamageForWeapon(other.gameObject.tag);
            if (damageToTake != 0)
            {
                _currentHp -= damageToTake;
            }

            if (_currentHp <= 0)
            {
                Destroy(gameObject, 0f);
                TroopDamage.DecrementEnemyCount();
                Instantiate(
                    xpPrefab, gameObject.transform.position, Quaternion.identity
                );
            }

            Destroy(other.gameObject, 0f);
        }

        private void OnParticleCollision(GameObject other)
        {
            float damageToTake = TroopDamage.GetDamageForWeapon(other.gameObject.tag);
            if (damageToTake != 0)
            {
                _currentHp -= damageToTake;
            }

            if (_currentHp <= 0)
            {
                Destroy(gameObject, 0f);
                TroopDamage.DecrementEnemyCount();
                Instantiate(
                    xpPrefab, gameObject.transform.position, Quaternion.identity
                );
            }
        }
    }
}