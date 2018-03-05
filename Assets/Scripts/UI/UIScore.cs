using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour {
    public Text playerOneScore;
    public Text playerTwoScore;

    private Player playerOne;
    private Player playerTwo;

    private string playerOneTextOriginal;
    private string playerTwoTextOriginal;

	void Start () {
        if (string.IsNullOrEmpty(playerOneScore.text) == false)
            playerOneTextOriginal = playerOneScore.text;

        if (string.IsNullOrEmpty(playerTwoScore.text) == false)
            playerTwoTextOriginal = playerTwoScore.text;

        Player[] players = FindObjectsOfType<Player>();

        foreach (Player p in players) {
            if (p.controller == XboxCtrlrInput.XboxController.First)
                playerOne = p;
            else if (p.controller == XboxCtrlrInput.XboxController.Second)
                playerTwo = p;
        }
	}
	
	void Update () {
        if (playerOne == null || playerTwo == null)
            return;

        UpdateScore();
	}

    private void UpdateScore() {
        playerOneScore.text = string.Format("{0}: {1}", playerOneTextOriginal, playerOne.GetScore());
        playerTwoScore.text = string.Format("{0}: {1}", playerTwoTextOriginal, playerTwo.GetScore());
    }
}
