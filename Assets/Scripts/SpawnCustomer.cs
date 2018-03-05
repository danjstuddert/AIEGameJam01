using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public GameObject customer;
    public Vector3 position;
    public float customerSpawnTimer;

    private List<GameObject> customerList;
    private float timer;

    // Use this for initialization
    void Awake()
    {
        customerList = new List<GameObject>();
        timer = 0.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer % customerSpawnTimer == 0.0f)
        {
            customerList.Add(Instantiate(customer, position, Quaternion.identity));
        }
    }
}
