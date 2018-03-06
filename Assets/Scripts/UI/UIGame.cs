using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour {
    public Text hotdogScore;
    public Text tacoScore;
    public GameObject pauseMenu;
    public GameObject firstSelectedButton;
    public GameObject inGameMenu;
	public GameObject hotdogWins;
	public GameObject tacoWins;
	public Text roundTimer;

    public bool IsPaused { get; private set; }
	public bool GameEnded { get; private set; }

    private Player playerOne;
    private Player playerTwo;

    private EventSystem eventSystem;
	private Timer timer;

	void Start () {
		Time.timeScale = 1f;
		Cursor.visible = false;

        if (pauseMenu.activeInHierarchy)
            pauseMenu.SetActive(false);

        if (inGameMenu.activeInHierarchy == false)
            inGameMenu.SetActive(true);

        Player[] players = FindObjectsOfType<Player>();

        foreach (Player p in players) {
            if (p.controller == XboxCtrlrInput.XboxController.First)
                playerOne = p;
            else if (p.controller == XboxCtrlrInput.XboxController.Second)
                playerTwo = p;
        }

        eventSystem = FindObjectOfType<EventSystem>();
        StartCoroutine(SelectFirstSelectable());
		timer = FindObjectOfType<Timer>();
	}
	
	void Update () {
        CheckInput();

        if (playerOne == null || playerTwo == null)
            return;
        UpdateScore();
		UpdateTimer();
	}

    public void Quit() {
        Application.Quit();
    }

    private void UpdateScore() {
		hotdogScore.text = playerOne.GetScore().ToString();
        tacoScore.text = playerTwo.GetScore().ToString();
	}

	private void UpdateTimer() {
		roundTimer.text = string.Format("{0}:{1}", timer.minutes, (int)timer.seconds);
	}

    private void CheckInput(){
        if (GameEnded == false && XCI.GetButtonDown(XboxButton.Start, XboxController.First) ||
			GameEnded == false && XCI.GetButtonDown(XboxButton.Start, XboxController.Second))
            TogglePause();

		if(GameEnded && XCI.GetButtonDown(XboxButton.Start, XboxController.First) ||
			GameEnded && XCI.GetButtonDown(XboxButton.Start, XboxController.Second) ||
			GameEnded && XCI.GetButtonDown(XboxButton.A, XboxController.First) ||
			GameEnded && XCI.GetButtonDown(XboxButton.A, XboxController.Second))
			SceneManager.LoadScene(0);
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

    public void ShowEndScreen() {
		Time.timeScale = 0f;
		GameEnded = true;

		if(playerOne.GetScore() > playerTwo.GetScore()) {
			hotdogWins.SetActive(true);
		} else {
			tacoWins.SetActive(true);
		}
    }

    private IEnumerator SelectFirstSelectable() {
        yield return new WaitForEndOfFrame();

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }
}
