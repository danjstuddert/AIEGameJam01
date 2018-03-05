using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public float effectiveRadius;
    private Transform playerTruck;
    private float timeSpentInAir;
    private Vector3 playerForward;
    public float force;
    public List<GameObject> ingredients;

	// Use this for initialization
	void Start () {
        timeSpentInAir = 2.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (timeSpentInAir > 0)
        {
            Vector3 newForce = playerForward * force;


            GetComponent<Rigidbody>().AddForce(newForce.normalized);
            
            timeSpentInAir -= Time.deltaTime;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Customers>())
        {
            //set customer target destination

        }

        //get random ingredient
        int rand = Random.Range(0, ingredients.Count);

        Vector3 vec = transform.position;
        vec.y = 0;
        Instantiate(ingredients[rand], vec, Quaternion.identity);

        Destroy(gameObject);
    }

    public void Create(Transform truckPosition, float forceAppliedToFood, Vector3 forward)
    {
        playerTruck = truckPosition;
        playerForward = forward;
        force = forceAppliedToFood;
    }

}
