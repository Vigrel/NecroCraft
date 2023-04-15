using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject villagersParent;

    public float speed = 2;
    public float maxHp;
    public RectTransform barImage;
    public float damageTimer = 0.1f;

    private float _currentHp;
    private float _movementX;
    private float _movementY;
    private float _HPbarMaxSize;
    private float _lastDamageTime;
    private Vector2 _startingHealthBarPosition;


    public void SetHealth()
    {
        float newSize = _HPbarMaxSize * (_currentHp / maxHp);

        //barImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize);
        //barImage.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 363.5f, newSize);

        barImage.sizeDelta = new Vector2(newSize, barImage.sizeDelta.y);
        barImage.anchoredPosition = new Vector2(_startingHealthBarPosition.x - (_HPbarMaxSize - newSize)/2,
            _startingHealthBarPosition.y);
    }

    void Start()
    {
        _HPbarMaxSize = barImage.rect.width;
        _currentHp = maxHp;
        _startingHealthBarPosition = barImage.anchoredPosition;
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(_movementX, _movementY);
        transform.Translate(movement * (speed * Time.deltaTime));
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        _movementX = v.x;
        _movementY = v.y;
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
            SetHealth();
        }
        
        if (_currentHp <= 0)
        {
            Time.timeScale = 0;
        }
    }
}