using UnityEngine;
using PlayerScripts;

namespace TroopScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] public GameObject prefab;
        [SerializeField] public float spawnRadius = 20f;
        [SerializeField] public float spawnDelay = 1f;
    
        private float _lastSpawnTime;

        protected virtual void Start()
        {
            _lastSpawnTime = Time.time;
        }

        protected virtual void Update()
        {
            if (!(Time.time - _lastSpawnTime >= spawnDelay)) return;
            if (TroopDamage.EnemyCount >= TroopDamage.MaxEnemyCount) return;
            
            _lastSpawnTime = Time.time;
            Spawn();
        }

        protected virtual void Spawn()
        {
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
            spawnPosition += (Vector2) PlayerController.Instance.Position;
            GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
            TroopDamage.IncrementEnemyCount();
        }
    }
}