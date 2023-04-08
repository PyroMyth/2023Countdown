using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private static GameObject pausePanel;
    private static Countdown countdownTimer;
    private static InputSystemInputs inputSystem;
    private static GameObject[] pauseButtons;
    void Awake() {
        pausePanel = GameObject.Find("Panel Pause Menu");
        pausePanel.SetActive(false);
        countdownTimer = GameObject.Find("Timer").GetComponent<Countdown>();
        inputSystem = GameObject.Find("PlayerCapsule").GetComponent<InputSystemInputs>();
        pauseButtons = GetPauseButtons();
    }

    private GameObject[] GetPauseButtons() {
        GameObject[] buttons = FindGameObjectsWithTag("Button");
        GameObject[] pauseButtons = new GameObject[];
        Transform pausePanelTransform = Find("Panel Pause Menu").transform;
        for (int i = 0; i < buttons.Length; i++) {
            if (buttons[i].transform.IsChildOf(pausePanelTransform)) {
                pauseButtons[pauseButtons.Length] = buttons[i];
            }
        }
    }

    public static void Toggle(bool isPaused) {
        if (isPaused) {
            countdownTimer.Restart();
            if (pausePanel != null) {
                pausePanel.SetActive(false);
            }
        } else {
            countdownTimer.Pause();
            if (pausePanel != null) {
                pausePanel.SetActive(true);
            }
        }
        //inputSystem.OnPause(null);
    }

    public static void ExitPauseMenu() {
        Toggle(false);
        inputSystem.OnPause(null);
    }

    public static void HandleClick(Vector3 mousePosition) {
        string clicking = IsClicking(mousePosition);
        if (clicking == "Return to Game") {

        } else if (clicking == "Main Menu") {

        }
    }

    private static string IsClicking(Vector3 position) {
        for (int i = 0; i < pauseButtons.Length; i++) {
            //if (position.)
        }
    }
}
