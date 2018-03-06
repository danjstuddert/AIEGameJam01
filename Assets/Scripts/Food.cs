using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    private float effectiveRadius;
    public Vector3 playerForward;
    public float horizontalForce;
    public float verticalForce;
    public List<GameObject> ingredients;
    public GameObject thisPlayer;
    private Player[] listOfPlayers;

	// Use this for initialization
	void Start () {
        listOfPlayers = FindObjectsOfType<Player>();

        for (int i = 0; i < listOfPlayers.Length; i++)
        {
            if (CompareTag("Hotdog") && listOfPlayers[i].controller == XboxCtrlrInput.XboxController.First)
            {
                thisPlayer = listOfPlayers[i].gameObject;
                playerForward = thisPlayer.transform.forward;
            }
        }

        Vector3 newForce = playerForward * horizontalForce;
        newForce.y += verticalForce;
        GetComponent<Rigidbody>().AddForce(newForce);

    }

    // Update is called once per frame
    void FixedUpdate () {
        //updates playerForward
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        //get random ingredient
        int rand = Random.Range(0, ingredients.Count);

        if (collision.gameObject.GetComponent<Customers>() == null && collision.gameObject.GetComponent<Player>() == null)
        {
            Vector3 vec = transform.position;
            vec.y = collision.transform.position.y;

            Instantiate(ingredients[rand], vec, Quaternion.identity);
        }        

        Destroy(gameObject);
    }
}
