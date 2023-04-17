using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] int speed;

    private Vector3 _direction;

    private void Update() {
        // transform.Translate(Vector3.one * (speed * Time.deltaTime));
        transform.Translate(_direction * (speed * Time.deltaTime));
    }

    public void SetDirection(Vector3 direction){
        _direction = direction;        
    }
}