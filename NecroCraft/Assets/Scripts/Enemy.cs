using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1;
    [SerializeField] public float hp = 5;
    [SerializeField] public float damage = 1;
    
    private Transform _objectToFollow;
    private bool _isobjectToFollowNull = true;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_isobjectToFollowNull) return;
        transform.position = Vector3.MoveTowards(transform.position, _objectToFollow.position,
            moveSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform player)
    {
        _objectToFollow = player;
        _isobjectToFollowNull = false;
    }

}
