using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    private float effectiveRadius;
    private Transform playerTruck;
    private Vector3 playerForward;
    public float horizontalForce;
    public float verticalForce;
    public List<GameObject> ingredients;

	// Use this for initialization
	void Start () {
        Vector3 newForce = playerForward * horizontalForce;
        newForce.y += 3;
        GetComponent<Rigidbody>().AddForce(newForce);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        //get random ingredient
        int rand = Random.Range(0, ingredients.Count);

        Vector3 vec = transform.position;

        Destroy(gameObject);
    }

    public void Create(Transform truckPosition, Vector3 forward)
    {
        playerTruck = truckPosition;
        playerForward = forward;
    }

}
