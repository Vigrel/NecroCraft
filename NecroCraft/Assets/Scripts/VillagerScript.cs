using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    public GameObject objectToFollow;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = objectToFollow.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.transform.position, moveSpeed * Time.deltaTime);
    }
}
