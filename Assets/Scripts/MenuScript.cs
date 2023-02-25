using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {
    [Header("Scene Names")]
    public string tutorialScene;
    public string gameScene;

    // private List<GameObject> panels;
    private Dictionary<string, string> panelAKAs = new Dictionary<string, string>(){
        {"main", "Panel Main Menu"},
        {"level", "Panel Select Level"},
        // {"score": "Panel High Score"},
        {"options", "Panel Options"},
        {"credits", "Panel Credits"}
    };

    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    void Awake() {
        // Find the panels
        foreach (KeyValuePair<string, string> kvp in panelAKAs) {
            Debug.Log("key=" + kvp.Key + " value=" + kvp.Value);
            panels.Add(kvp.Key, GameObject.Find(kvp.Value));
            Debug.Log(kvp.Key + " = " + panels[kvp.Key].name);
        }
        // mainMenu = GameObject.Find("Panel Main Menu");
        // levelSelect = GameObject.Find("Panel Level Select");
        // highScores = GameObject.Find("Panel High Score");
        // options = GameObject.Find("Panel Options");
        // credits = GameObject.Find("Panel Credits");
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
        
        // TODO: Load the high scores
        // HighScores.Load();
    }

    public void ClickCredits() {
        ActivatePanel("credits");
    }

    public void ClickStartTutorial() {
        // TODO: Load the Tutorial level
        SceneManager.LoadScene(tutorialScene);
    }

    public void ClickStartGame() {
        // TODO: Load the main game
        SceneManager.LoadScene(gameScene);
    }

    public void ClickBackToMenu() {
        // TODO: Return to the Main Menu panel
        ActivatePanel("main");
    }

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
