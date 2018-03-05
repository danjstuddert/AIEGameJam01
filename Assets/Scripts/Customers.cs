using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customers : MonoBehaviour
{
    public ParticleSystem effect;
    public string redFood;
    public string greenFood;

    private NavMeshAgent agent;
    private GameObject screenTop;
    private GameObject screenBottom;
    private GameObject redTruck;
    private GameObject greenTruck;
    private bool customerHit;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        customerHit = false;
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (customerHit && agent.remainingDistance == 0 && (effect != null))
        {
            effect.Play();
        }

        if (!customerHit && agent.remainingDistance == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(redFood))
        {
            agent.destination = redTruck.transform.position;
            customerHit = true;
        }

        if (collision.gameObject.CompareTag(greenFood))
        {
            agent.destination = greenTruck.transform.position;
            customerHit = true;
        }
    }

    public void SetObjects(GameObject top, GameObject bottom, GameObject red, GameObject green)
    {
        screenTop = top;
        screenBottom = bottom;
        redTruck = red;
        greenTruck = green;

        SpawnCustomer spawnScript = FindObjectOfType<SpawnCustomer>();

        if (spawnScript.GetPosition().z == spawnScript.topSpawnZ)
        {
            agent.destination = screenBottom.transform.position;
        }

        if (spawnScript.GetPosition().z == spawnScript.bottomSpawnZ)
        {
            agent.destination = screenTop.transform.position;
        }
    }
}
