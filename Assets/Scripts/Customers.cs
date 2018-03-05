using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customers : MonoBehaviour
{
    public Transform truck;

	// Use this for initialization
	void Awake()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = truck.position;
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        
    }
}
