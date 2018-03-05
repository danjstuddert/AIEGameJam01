using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 movementVector;
    public float movementSpeed;

    public Transform truckPosition;
    private float score;
    public float cooldown;
    private float timeRemaining;

    public XboxController controller;
    public Quaternion rotation;

    public GameObject temp;
    public float forceAppliedToFood;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        RotatePlayer();

        movementVector.z = XCI.GetAxis(XboxAxis.LeftStickY, controller);
        movementVector.x = XCI.GetAxis(XboxAxis.LeftStickX, controller);
                
        rb.MovePosition(rb.position + movementVector * movementSpeed * Time.fixedDeltaTime);
                
        if (timeRemaining <= 0)
        {
            if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
            {
                Attack();
                timeRemaining = cooldown;
            }
        }       

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }        
    }

    private void RotatePlayer()
    {
        float rotateAxisX = 0;
        float rotateAxisZ = 0;

        //get controller's right stick input
        rotateAxisX = XCI.GetAxis(XboxAxis.RightStickX, controller);
        rotateAxisZ = XCI.GetAxis(XboxAxis.RightStickY, controller);

        Vector3 normaliseRotate = new Vector3(rotateAxisX, 0, rotateAxisZ);
        //normaliseRotate = normaliseRotate.normalized;
        if (normaliseRotate.normalized.x != 0 || normaliseRotate.normalized.z != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.Normalize(normaliseRotate));
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
        //instantiate object with rigidbody
        Instantiate(temp, transform.position + transform.forward, Quaternion.identity);
        
    }
}
