using UnityEngine;
using PlayerScripts;

namespace TroopScripts
{
    public class AllySpawner : MonoBehaviour
    {
        [SerializeField] public GameObject prefab;
        [SerializeField] public float spawnRadius = 5f;
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
            if (TroopDamage.AllyCount >= TroopDamage.MaxAllyCount) return;
            Spawn();
        }

        protected virtual void Spawn()
        {
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
            spawnPosition += (Vector2) PlayerController.Instance.Position;
            GameObject ally = Instantiate(prefab, spawnPosition, Quaternion.identity);
            TroopDamage.IncrementAllyCount();
        }
        
        public void UpdateSpawnDelay(float delayReductionPct)
        {
            spawnDelay *= (1f - delayReductionPct);
        }
    }
}

