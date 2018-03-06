using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Player : MonoBehaviour {

    private Animator animator;

    private Rigidbody rb;
    private Vector3 movementVector;
    public float movementSpeed;

    public Transform truckPosition;
    private float score;
    public float cooldown;
    private float timeRemaining;

    public XboxController controller;

    public GameObject food;
    public float distanceOffset;
    private bool throwing;
    private float throwTime;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        throwTime = 0.5f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        RotatePlayer();

        //get controller's left analog input
        movementVector.z = XCI.GetAxis(XboxAxis.LeftStickY, controller);
        movementVector.x = XCI.GetAxis(XboxAxis.LeftStickX, controller);
        
        if (movementVector.z != 0 || movementVector.x != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        if (movementVector.z == 0 && movementVector.x == 0)
        {
            animator.SetBool("IsRunning", false);
        }

        if (throwing == true && throwTime > 0)
        {
            throwTime -= Time.deltaTime;
        }
        if (throwing == true && throwTime <= 0)
        {
            throwing = false;
            animator.SetBool("IsThrowing", false);
            throwTime = 0.5f;
        }            

        rb.MovePosition(rb.position + movementVector * movementSpeed * Time.fixedDeltaTime);
        
        //if no more time remaining, allow attack
        if (timeRemaining <= 0)
        {
            if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f || Input.GetKey(KeyCode.Space))
            {
                Attack();
                timeRemaining = cooldown;
            }            
        }

        //reduce time remaining
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
        //check if player 1 and food attached is a hotdog
        if (food.CompareTag("Hotdog") && controller == XboxCtrlrInput.XboxController.First)
        {
            //create new hotdog object in front of player
            Instantiate(food, transform.position + transform.forward * distanceOffset, Quaternion.identity);

            ThrowAnimation();
        }

        //check if player 2 and food attached is a taco
        else if (food.CompareTag("Taco") && controller == XboxCtrlrInput.XboxController.Second)
        {
            //create new taco object in front of player
            Instantiate(food, transform.position + transform.forward * distanceOffset, Quaternion.identity);

            ThrowAnimation();
        }        
    }

    private void ThrowAnimation()
    {
        animator.SetTrigger("IsThrowing");        
    }
}
