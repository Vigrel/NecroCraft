using UnityEngine;
using PlayerScripts;

namespace Projectile
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject prefab;
        [SerializeField] public float spawnDelay = 1f;
    
        private float _lastSpawnTime;

        protected virtual void Start()
        {
            _lastSpawnTime = Time.time;
        }

        protected virtual void Update()
        {   
            if (!(Time.time - _lastSpawnTime >= spawnDelay)) return;
            _lastSpawnTime = Time.time;
            Spawn();
        }

        protected virtual void Spawn()
        {
            GameObject projectile = Instantiate(
                prefab, PlayerController.Instance.Position, Quaternion.identity
                );
        }
        
        public void UpdateSpawnDelay(float delayReductionPct)
        {
            spawnDelay *= (1f - delayReductionPct);
        }
    }
}