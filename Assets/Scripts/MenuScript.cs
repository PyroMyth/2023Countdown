using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {
    [Header("Scene Names")]
    public string tutorialScene;
    public string gameScene;

    // A map of a short name for a panel to the full name of the GameObject
    private Dictionary<string, string> panelAKAs = new Dictionary<string, string>(){
        {"main", "Panel Main Menu"},
        {"level", "Panel Select Level"},
        {"scores", "Panel High Score"},
        {"options", "Panel Options"},
        {"credits", "Panel Credits"}
    };

    // A map of the short name for a panel to the GameObject containing the panel
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    void Awake() {
        // Find the panels and populate the map
        foreach (KeyValuePair<string, string> kvp in panelAKAs) {
            panels.Add(kvp.Key, GameObject.Find(kvp.Value));
        }
    }

    void Start() {
        ActivatePanel("main");
    }

    public void ClickLevelSelect() {
        ActivatePanel("level");
    }

    public void ClickOptions() {
        ActivatePanel("options");
    }

    public void ClickHighScores() {
        ActivatePanel("scores");
    }

    public void ClickCredits() {
        ActivatePanel("credits");
    }

    public void ClickStartTutorial() {
        // Load the Tutorial level
        SceneManager.LoadScene(tutorialScene);
    }

    public void ClickStartGame() {
        SceneManager.LoadScene(gameScene);
    }

    public void ClickBackToMenu() {
        // Return to the Main Menu panel
        ActivatePanel("main");
    }

    /**
      *
      * Helper method to activate a panel.
      *
      * This method loops through all panels
      * found in the Awake method and disables
      * all except the specified panel
      *
      * @param panel - the short name of the panel
      *                 that should be made visible
      *                 All other panels will be
      *                 disabled
      *
      */
    private void ActivatePanel(string panel) {
        foreach (KeyValuePair<string, GameObject> kvp in panels) {
            if (kvp.Key.ToLower() != panel.ToLower()) {
                panels[kvp.Key].SetActive(false);
            } else {
                panels[kvp.Key].SetActive(true);
            }
        }
    }
}
