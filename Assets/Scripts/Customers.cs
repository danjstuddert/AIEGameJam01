using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customers : MonoBehaviour
{
    public ParticleSystem effect;
    public string redFood = "Hotdog";
    public string greenFood = "Taco";

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
        float dist = agent.remainingDistance;

        if (customerHit && agent.remainingDistance == 0 && (effect != null))
        {
            effect.Play();
        }
        if (customerHit)
        {
            Debug.Log("Customer Hit");
        }
        if (!customerHit && (agent.remainingDistance < 0.1f) && (dist != Mathf.Infinity) && (agent.pathStatus == NavMeshPathStatus.PathComplete))
        {
            Debug.Log("Destrying");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (redFood != null)
        {
            if (collision.gameObject.CompareTag(redFood))
            {
                agent.destination = redTruck.transform.position;
                customerHit = true;
            }
        }

        if (greenFood != null)
        {
            if (collision.gameObject.CompareTag(greenFood))
            {
                agent.destination = greenTruck.transform.position;
                customerHit = true;
            }
        }
    }

    public void SetObjects(GameObject top, GameObject bottom, GameObject red, GameObject green)
    {
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (agent == null)
        {
            Debug.Log("Agent Doesnt Exist");
        }


        screenTop = top;
        screenBottom = bottom;
        redTruck = red;
        greenTruck = green;

        SpawnCustomer spawnScript = FindObjectOfType<SpawnCustomer>();

        if (spawnScript == null)
        {
            Debug.Log("spawnScript Doesnt Exist");
        }

        if (Mathf.Abs(spawnScript.GetPosition().z - spawnScript.topSpawnZ) < 0.01f)
        {
            agent.destination = screenBottom.transform.position;
        }

        if (Mathf.Abs(spawnScript.GetPosition().z - spawnScript.bottomSpawnZ) < 0.01f)
        {
            
                //Debug.LogError(agent.isActiveAndEnabled.ToString());
            
            agent.destination = screenTop.transform.position;
        }
    }
}
