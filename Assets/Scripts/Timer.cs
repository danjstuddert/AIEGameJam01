using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float minutes;
    public float seconds;
    public bool isGameRunning;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        seconds -= Time.deltaTime;

        if (isGameRunning)
        {
            FindObjectOfType<UIGame>().ShowEndScreen();
        }

        if (seconds <= 0)
        {
            if (minutes > 0)
            {
                seconds = 60;
                minutes--;
            }
            else
            {
                isGameRunning = false;
            }
        }
	}
}
