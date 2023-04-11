using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameObject villagersParent;

    public float speed = 1;
    public float hp = 10;
    public float dp = 5;

	private float movementX;
	private float movementY;

	void Start ()
	{
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
    void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.tag == "Villager"){
			hp -= 1;
			other.transform.position = new Vector3(0f, 2f, 0f);
		}
		if(hp==0){
			Time.timeScale = 0;
		}
	}
}