using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float spawnDelay = 1f;
    
    private Transform _player;
    private float _lastSpawnTime;
    private Vector3 _lastPlayerPosition;
    private Vector3 _direction = new Vector3(1,1,0);

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
        Vector3 currentPlayerPosition = _player.transform.position;
        Vector3 stoped = currentPlayerPosition - _lastPlayerPosition;

        if(stoped.x != 0 || stoped.y !=0){
            _direction = currentPlayerPosition - _lastPlayerPosition;
        }
        _lastPlayerPosition = currentPlayerPosition;
        Debug.Log(_direction);

        GameObject projectile = Instantiate(prefab, _player.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(_direction.normalized);
    }
}