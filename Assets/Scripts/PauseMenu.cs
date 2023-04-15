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
        pauseButtons = GetPauseButtons();
        pausePanel.SetActive(false);
        countdownTimer = GameObject.Find("Timer").GetComponent<Countdown>();
        inputSystem = GameObject.Find("PlayerCapsule").GetComponent<InputSystemInputs>();
    }

    private GameObject[] GetPauseButtons() {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        GameObject[] pauseButtons = new GameObject[3];
        int pauseButtonsCount = 0;
        for (int i = 0; i < buttons.Length; i++) {
            if (buttons[i].transform.IsChildOf(pausePanel.transform)) {
                pauseButtons[pauseButtonsCount] = buttons[i];
                pauseButtonsCount++;
            }
        }
        return pauseButtons;
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
        if (clicking == "Button Return to Game") {

        } else if (clicking == "Button Main Menu") {

        }
    }

    private static string IsClicking(Vector3 mousePos) {
        RectTransform buttonRect;
        Debug.Log("MousePosition: " + mousePos.x + "," + mousePos.y);
        mousePos = Camera.main.WorldToScreenPoint(mousePos);
        Debug.Log("MousePosition Screen: " + mousePos.x + "," + mousePos.y);
        mousePos = Camera.main.WorldToViewportPoint(mousePos);
        Debug.Log("MousePosition Viewport: " + mousePos.x + "," + mousePos.y);
        // Expecting pivot positions for Pause Menu buttons to be at 0,0 local coords
        for (int i = 0; i < pauseButtons.Length; i++) {
            buttonRect = pauseButtons[i].GetComponent<RectTransform>();
            Debug.Log("Rect: " + buttonRect.rect.x + "," + buttonRect.rect.y + "," + "," + buttonRect.rect.width + "," + buttonRect.rect.height);
            if (mousePos.x >= buttonRect.rect.x && 
                mousePos.x <= buttonRect.rect.x + buttonRect.rect.width &&
                mousePos.y >= buttonRect.rect.y && 
                mousePos.y <= buttonRect.rect.y + buttonRect.rect.height) {
                return pauseButtons[i].name;
            }
        }
        return "";
    }
}
