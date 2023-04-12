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

    private float currentHp;
	private float movementX;
	private float movementY;
	private float HPbarMaxSize;


    public void SetHealth()
    {
		float newSize = HPbarMaxSize*(currentHp/maxHp);
		barImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize);
    }

	void Start ()
	{
		HPbarMaxSize = barImage.rect.width;
		currentHp = maxHp;
	}

    // Start is called before the first frame update
	void FixedUpdate ()
	{
		Vector2 movement = new Vector2 (movementX, movementY);
		transform.Translate(movement * speed * Time.deltaTime);
		
	}
	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}
    void OnCollisionStay2D(Collision2D other) 
	{
		if(other.gameObject.tag == "Villager"){
			currentHp -= 1.0f;
			SetHealth();
		}
		if(currentHp==0){
			Time.timeScale = 0;
		}
	}
}