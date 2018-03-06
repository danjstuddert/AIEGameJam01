using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public AudioSource music;
    public GameObject[] customers;
    public GameObject screenTop;
    public GameObject screenBottom;
    public GameObject redTruck;
    public GameObject greenTruck;
    public float minSpawnX = -10.0f;
    public float maxSpawnX = 10.0f;
    public float spawnY = 0.0f;
    public float bottomSpawnZ = -10.0f;
    public float topSpawnZ = 10.0f;
    public float spawnTimer = 2.0f;

    private List<GameObject> customerList;
    private Vector3 position;
    private float timer;

    // Use this for initialization
    void Awake()
    {
        customerList = new List<GameObject>();
        timer = spawnTimer;

        if (music != null)
            music.Play();
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            int randPos = Random.Range(0, 2);
            int randCust = Random.Range(0, customers.Length);

            if (randPos == 0)
            {
                position = new Vector3(Random.Range(minSpawnX, maxSpawnX),
                                       spawnY, bottomSpawnZ);
            }

            if (randPos == 1)
            {
                position = new Vector3(Random.Range(minSpawnX, maxSpawnX),
                                       spawnY, topSpawnZ);
            }
            
            GameObject c = Instantiate(customers[randCust], position, Quaternion.identity);           
            customerList.Add(c);
            c.GetComponent<Customers>().SetObjects(screenTop, screenBottom, 
                                                   redTruck, greenTruck);
            timer = spawnTimer;
        }
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}
