using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customers : MonoBehaviour
{
    public ParticleSystem effect;
    public string redFood = "Hotdog";
    public string greenFood = "Taco";
    public AudioClip addScore;

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
        if (customerHit && (agent.remainingDistance < 0) && effect != null)
        {
            effect.Play();
            AudioSource.PlayClipAtPoint(addScore, transform.position);
        }

        if (!customerHit && agent.remainingDistance < 0.1f)
        {
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
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        screenTop = top;
        screenBottom = bottom;
        redTruck = red;
        greenTruck = green;

        SpawnCustomer spawnScript = FindObjectOfType<SpawnCustomer>();

        if (Mathf.Abs(spawnScript.GetPosition().z - spawnScript.topSpawnZ) < 0.01f)
        {
            agent.destination = screenBottom.transform.position;
        }

        if (Mathf.Abs(spawnScript.GetPosition().z - spawnScript.bottomSpawnZ) < 0.01f)
        {
            agent.destination = screenTop.transform.position;
        }
    }
}
