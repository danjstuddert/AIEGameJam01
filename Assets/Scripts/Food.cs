using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public float launchSpeed;
    public Transform playerTruck;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddExplosionForce(launchSpeed, transform.position, 5, 2);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Customers>())
        {
            //set target to shooter's truck
        }

        Destroy(gameObject);
    }

    public void Create(GameObject player)
    {
        
    }

}
