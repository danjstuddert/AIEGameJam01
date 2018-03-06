using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {
    public Text hotdogScore;
    public Text tacoScore;
    public GameObject pauseMenu;
    public GameObject firstSelectedButton;
    public GameObject inGameMenu;

    public bool IsPaused { get; private set; }

    private Player playerOne;
    private Player playerTwo;

    private string playerOneTextOriginal;
    private string playerTwoTextOriginal;
    private EventSystem eventSystem;

	void Start () {
        if (pauseMenu.activeInHierarchy)
            pauseMenu.SetActive(false);

        if (inGameMenu.activeInHierarchy == false)
            inGameMenu.SetActive(true);

        if (string.IsNullOrEmpty(hotdogScore.text) == false)
            playerOneTextOriginal = hotdogScore.text;

        if (string.IsNullOrEmpty(tacoScore.text) == false)
            playerTwoTextOriginal = tacoScore.text;

        Player[] players = FindObjectsOfType<Player>();

        foreach (Player p in players) {
            if (p.controller == XboxCtrlrInput.XboxController.First)
                playerOne = p;
            else if (p.controller == XboxCtrlrInput.XboxController.Second)
                playerTwo = p;
        }

        eventSystem = FindObjectOfType<EventSystem>();
        StartCoroutine(SelectFirstSelectable());
	}
	
	void Update () {
        CheckInput();

        if (playerOne == null || playerTwo == null)
            return;
        UpdateScore();
	}

    public void Quit() {
        Application.Quit();
    }

    private void UpdateScore() {
        hotdogScore.text = string.Format("{0}: {1}", playerOneTextOriginal, playerOne.GetScore());
        tacoScore.text = string.Format("{0}: {1}", playerTwoTextOriginal, playerTwo.GetScore());
    }

    private void CheckInput(){
        if (XCI.GetButtonDown(XboxButton.Start, XboxController.First) || XCI.GetButtonDown(XboxButton.Start, XboxController.Second))
            TogglePause();
    }

    public void TogglePause() {
        // If paused turn off menu
        if (IsPaused) {
            Time.timeScale = 1f;
            IsPaused = false;

            pauseMenu.SetActive(false);
            inGameMenu.SetActive(true);
        } else {
            Time.timeScale = 0f;
            IsPaused = true;

            pauseMenu.SetActive(true);
            inGameMenu.SetActive(false);
            StartCoroutine(SelectFirstSelectable());
        }
    }

    private IEnumerator SelectFirstSelectable() {
        yield return new WaitForEndOfFrame();

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }
}
