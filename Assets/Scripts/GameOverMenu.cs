using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : PanelWithButtons {
    public void Awake() {
        Init();
        myPanel = gameObject;
        GetMyButtons();
        myPanel.SetActive(false);
        inputSystem = GameObject.Find("PlayerCapsule").GetComponent<InputSystemInputs>();
    }

    public void ToggleEndGame(float score) {
        inputSystem.EndGame();
        myPanel.SetActive(true);
        // Populate the final score info in the panel
        myPanel.transform.Find("Game Over Score Value").GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString("n0");
        // If it's a high score, collect the player's name
        if (GameData.IsHighScore(score)) {
            myPanel.transform.Find("Panel High Score").gameObject.SetActive(true);
        }
    }

    public override void HandleClick(Vector3 mousePosition) {
        string clicking = IsClicking(mousePosition);
        if (clicking.ToLower().IndexOf("Main Menu".ToLower()) > -1) {
            Debug.Log("Returning to Main Menu");
        } else if (clicking.ToLower().IndexOf("Play Again".ToLower()) > -1) {
            Debug.Log("Playing Again");
        } else if (clicking.ToLower().IndexOf("Save My Score".ToLower()) > -1) {
            Debug.Log("Saving your score");
        }
    }
}