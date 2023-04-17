using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float spawnDelay = 1f;
    
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
        GameObject projectile = Instantiate(prefab, _player.position, Quaternion.identity);
    }
}