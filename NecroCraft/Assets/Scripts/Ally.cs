using System;
using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float maxHp = 5f;
    [SerializeField] public float damageTimer = 0.1f;

    private GameObject _player;
    private Vector3 _initialDistanceFromPlayer;
    private Vector3 _lastPlayerPosition;
    private float _currentHp;
    private float _lastDamageTime;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lastPlayerPosition = _player.transform.position;
        _initialDistanceFromPlayer = _lastPlayerPosition - transform.position;
        _currentHp = maxHp;
    }

    private void Update()
    {
        Vector3 currentPlayerPosition = _player.transform.position;

        if (currentPlayerPosition.x > _lastPlayerPosition.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (currentPlayerPosition.x < _lastPlayerPosition.x)
            transform.localScale = new Vector3(-1, 1, 1);

        _lastPlayerPosition = currentPlayerPosition;
        Vector3 targetPosition = currentPlayerPosition - _initialDistanceFromPlayer;
        transform.position = Vector3.MoveTowards(
                    transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        float damageElapsedTime = Time.fixedTime - _lastDamageTime;
        if (damageElapsedTime < damageTimer) return;

        float damageToTake = TroopDamage.GetDamageForEnemy(other.gameObject.tag);

        if (damageToTake != 0)
        {
            _currentHp -= damageToTake;
            _lastDamageTime = Time.fixedTime;
        }

        if (_currentHp <= 0)
        {
            Destroy(gameObject, 0f);
        }
    }
}