using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public AudioClip splatNoise;
    private float effectiveRadius;
    public Vector3 playerForward;
    public float horizontalForce;
    public float verticalForce;
    public List<GameObject> ingredients;
    private GameObject thisPlayer;
    private Player[] listOfPlayers;

	// Use this for initialization
	void Start () {
        //return all players
        listOfPlayers = FindObjectsOfType<Player>();

        //set forward direction to player's current forward
        for (int i = 0; i < listOfPlayers.Length; i++)
        {
            if (CompareTag("Hotdog") && listOfPlayers[i].controller == XboxCtrlrInput.XboxController.First)
            {
                thisPlayer = listOfPlayers[i].gameObject;
                playerForward = thisPlayer.transform.forward;
            }
            else if (CompareTag("Taco") && listOfPlayers[i].controller == XboxCtrlrInput.XboxController.Second)
            {
                thisPlayer = listOfPlayers[i].gameObject;
                playerForward = thisPlayer.transform.forward;
            }
        }

        //apply force in direction
        Vector3 newForce = playerForward * horizontalForce;
        
        newForce.y += verticalForce;
        GetComponent<Rigidbody>().AddForce(newForce);
    }

    // Update is called once per frame
    void FixedUpdate () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (ingredients.Count != 0)
        {
            //get random ingredient
            int rand = Random.Range(0, ingredients.Count - 1);
            
            //do not create new object on players or customers
            if (collision.gameObject.GetComponent<Customers>() == null &&
                collision.gameObject.GetComponent<Player>() == null &&
                collision.gameObject.GetComponent<Food>() == null)
            {
                Vector3 vec = transform.position;
                vec.y = collision.transform.position.y;

                Instantiate(ingredients[rand], vec, Quaternion.identity);

                AudioSource.PlayClipAtPoint(splatNoise, transform.position);
            }
        }

        Destroy(gameObject);
    }
}
