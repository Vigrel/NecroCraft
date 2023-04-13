using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float spawnRadius = 20f;
    [SerializeField] public float spawnDelay = 1f;
    [SerializeField] public float spawnRate = 1f;
    
    private Transform _player;
    private float _lastSpawnTime;

    protected virtual void Start()
    {
        _lastSpawnTime = Time.time;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (!(Time.time - _lastSpawnTime >= spawnDelay)) return;
        _lastSpawnTime = Time.time;
        Spawn();
    }

    protected virtual void Spawn()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
        spawnPosition += (Vector2)_player.position;
        GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<VillagerScript>().SetTarget(_player);
    }
}