using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 movementVector;
    public float movementSpeed;
    public GameObject truckPosition;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //temporary keyboard movement
		if (Input.GetKeyDown(KeyCode.W))
        {
            movementVector.z = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movementVector.z = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movementVector.x = 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movementVector.x = -1;
        }

        rb.AddForce(movementVector * movementSpeed);

    }
}
