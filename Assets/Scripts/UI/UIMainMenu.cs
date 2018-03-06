using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIMainMenu : MonoBehaviour {
    public GameObject firstSelectedButton;

    private EventSystem eventSystem;

    private void Start() {
        eventSystem = FindObjectOfType<EventSystem>();

        StartCoroutine(SelectFirstSelectable());
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }

    private IEnumerator SelectFirstSelectable() {
        yield return new WaitForEndOfFrame();

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }
}
