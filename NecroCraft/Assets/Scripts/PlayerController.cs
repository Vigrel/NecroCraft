using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
	private float movementX;
	private float movementY;

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
	
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     transform.Translate(Vector3.forward * speed * Time.deltaTime);
    // }
}