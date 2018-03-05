using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 movementVector;
    public float movementSpeed;

    public GameObject truckPosition;
    private float score;
    public float cooldown;
    private float timeRemaining;

    public XboxController controller;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        movementVector.z = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);
        movementVector.x = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);

        rb.MovePosition(rb.position + movementVector * movementSpeed * Time.fixedDeltaTime);

        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
        {
            Attack();
        }
        
        if (timeRemaining <= 0)
        {
            if (XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0.1f)
            {
                Attract();
                timeRemaining = cooldown;
            }
            
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

        }
        
    }

    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }

    public float GetScore()
    {
        return score;
    }

    public void Attack()
    {
        
    }

    public void Attract()
    {
        
    }
}
