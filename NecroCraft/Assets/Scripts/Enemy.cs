using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1;
    [SerializeField] public float hp = 5;
    [SerializeField] public float damage = 1;
    [SerializeField] public float maxDistance = 30f;

    private Transform _objectToFollow;
    private bool _isobjectToFollowNull = true;

    // Update is called once per frame
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
        if (distanceToPlayer > maxDistance) Destroy(this);
    }
}