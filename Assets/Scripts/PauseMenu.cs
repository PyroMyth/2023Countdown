using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private static GameObject pausePanel;
    private static Countdown countdownTimer;
    private static InputSystemInputs inputSystem;
    private static GameObject[] pauseButtons;
    private static LevelLoader levelLoad;
    
    void Awake() {
        pausePanel = GameObject.Find("Panel Pause Menu");
        pauseButtons = GetPauseButtons();
        pausePanel.SetActive(false);
        countdownTimer = GameObject.Find("Timer").GetComponent<Countdown>();
        inputSystem = GameObject.Find("PlayerCapsule").GetComponent<InputSystemInputs>();
        levelLoad = GameObject.Find("GameCanvas").GetComponent<LevelLoader>();
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
    }

    public static void ExitPauseMenu() {
        inputSystem.OnPause(null);
    }

    public static void HandleClick(Vector3 mousePosition) {
        string clicking = IsClicking(mousePosition);
        if (clicking.IndexOf("Return to Game") > -1) {
            ExitPauseMenu();
        } else if (clicking.IndexOf("Main Menu") > -1) {
            levelLoad.LoadLevel("GameMenu");
        }
    }

    private static string IsClicking(Vector3 mousePos) {
        RectTransform buttonRect;
        Vector3 origMousePos = mousePos;
        Debug.Log("MousePosition: " + mousePos.x + "," + mousePos.y);
        
        // Expecting pivot positions for Pause Menu buttons to be at 0,0 local coords
        Vector3[] corners = new Vector3[4];
        for (int i = 0; i < pauseButtons.Length; i++) {
            if (pauseButtons[i] != null) {
                buttonRect = pauseButtons[i].GetComponent<RectTransform>();
                buttonRect.GetWorldCorners(corners);
                Debug.Log("Button Corners:" + corners[0] + "," + corners[1] + "," + corners[2] + "," + corners[3]);
                if (mousePos.x >= corners[0].x && 
                    mousePos.x <= corners[2].x &&
                    mousePos.y >= corners[0].y && 
                    mousePos.y <= corners[1].y) {
                    Debug.Log("Found it!");
                    return pauseButtons[i].name;
                }
            }
        }
        return "";
    }
}