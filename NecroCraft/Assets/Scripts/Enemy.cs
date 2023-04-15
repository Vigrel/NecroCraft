using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1;
    [SerializeField] public float maxHp = 5;
    [SerializeField] public float maxDistance = 30f;
    [SerializeField] public float damageTimer = 0.1f;

    private Transform _objectToFollow;
    private bool _isobjectToFollowNull = true;
    private float _currentHp;
    private float _lastDamageTime;

    private void Start()
    {
        _currentHp = maxHp;
    }

    protected virtual void Update()
    {
        if (_isobjectToFollowNull) return;

        var playerPos = _objectToFollow.position;
        var selfPos = transform.position;

        DestroyWhenFarAway(selfPos, playerPos);
        MoveTowardsPlayer(selfPos, playerPos);
    }

    public void SetTarget(Transform player)
    {
        _objectToFollow = player;
        _isobjectToFollowNull = false;
    }

    private void MoveTowardsPlayer(Vector3 selfPos, Vector3 playerPos)
    {
        transform.position = Vector3.MoveTowards(
            selfPos, playerPos, moveSpeed * Time.deltaTime);
    }

    private void DestroyWhenFarAway(Vector3 selfPos, Vector3 playerPos)
    {
        var distanceToPlayer = Vector3.Distance(selfPos, playerPos);
        if (distanceToPlayer > maxDistance) Destroy(gameObject, 0f);
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
            Debug.Log("Destroying beetle");
            Destroy(gameObject, 0f);
        }
    }
}