using UnityEngine;

namespace TroopScripts
{
    public class VillagerScript : MonoBehaviour
    {
        public float moveSpeed;
        public float hp = 5;
        public float dp = 1;
    
        private Transform _objectToFollow;
        private bool _isobjectToFollowNull = true;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
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
}
