using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameData : MonoBehaviour {
    // The maximum number of high scores to store
    private static int maxNumberOfHighScores = 6;
    // The internal reference to the High Scores data structure
    private static HighScores highScores;
    // The game objects displayed that contain the high score information
    private List<GameObject> scoreGameObjects;
    // Backup copies of the default high score objects for use when clearing the high scores
    private List<GameObject> clearedScoreGameObjects;

    public void Awake() {
        // Instantiate our backup list of the default high score objects
        clearedScoreGameObjects = new List<GameObject>();
        // Request the High Scores data from the XMLManager
        highScores = XMLManager.LoadScores();
        if (highScores.scores.Count > 0) {
            // Call the method to Load the retrieved High Scores
            LoadScores();
        }
        Debug.Log("Found " + highScores.scores.Count + " high scores.");
    }

    /**
      *
      * This method clears the internal representation of 
      * the High Scores data structure.
      * It also toggles the visibility of the backup copies 
      * of the default high score objects.
      *
      */
    public void ClearHighScores() {
        highScores = new HighScores();
        XMLManager.SaveScores(highScores);
        for (int i = 0; i < clearedScoreGameObjects.Count; i++) {
            clearedScoreGameObjects[i].SetActive(true);
            scoreGameObjects[i].SetActive(false);
        }
    }

    /**
      * 
      * This method compares the provided value to the existing 
      * high scores to determine whether the player has achieved
      * a new high score.
      *
      * @param scoreValue - a float containing the player's score
      *
      * @return a Boolean - true if the provided score is higher
      *         than at least one of the existing high scores,
      *         false otherwise
      *
      */ 
    public static bool IsHighScore(float scoreValue) {
        // Provided value is a high score if:
        // It is greater than 0, and
        // It is greater than the last high score value
        return (scoreValue > 0 && maxNumberOfHighScores > 0 && 
                    ((highScores.scores.Count < maxNumberOfHighScores) ||
                    (highScores.scores.Count == maxNumberOfHighScores && 
                    scoreValue > highScores.scores[highScores.scores.Count - 1].score)));
    }

    /**
      *
      * This method accepts the player's name and their score, verifies that
      * it is a high score, then adds the score to the list of High Scores.
      * 
      * @param gameName - a String containing the name of the player
      * @param scoreValue - a float containing the player's score
      *
      */
    public static void SubmitHighScore(string gamerName, float scoreValue) {
        if (IsHighScore(scoreValue)) {
            HighScore highScore = new HighScore(gamerName, scoreValue);
            highScores.AddScore(highScore);
        }
    }

    /**
      *
      * This method accepts a list of GameObjects that display the High Scores
      * and updates the high score information shown on the High Scores panel
      *
      * @param scoreObjects - a List of GameObjects that contains two children:
      *             one called Name with a TextMeshPro component and
      *             one called Score with a TextMeshPro component
      *
      * @ return List of GameObjects that are active and displayed on the
      *             High Score panel of the Main Menu
      *
      */
    public static List<GameObject> GetHighScores(List<GameObject> scoreObjects) {
        List<GameObject> retVal = new List<GameObject>();
        if (highScores.scores.Count > 0) {
            for (int i = 0; i < highScores.scores.Count; i++) {
                retVal.Add(GetHighScoreGameObject(highScores.scores[i], scoreObjects[i]));
            }
        }
        if (retVal.Count < scoreObjects.Count) {
            for (int i = retVal.Count - 1; i < scoreObjects.Count; i++) {
                retVal.Add(scoreObjects[i]);
            }
        }
        return retVal;
    }

    /**
      *
      * Helper method to load the scores and display to the user.
      *
      * Calls the GetScoreGameObjects helper method to find all 
      * GameObjects designated as High Score game objects.
      *
      * Populates the list of backup default high score objects
      *
      * Calls the GetHighScores helper method to instantiate and
      * populate the High Score data that was loaded
      *
      */
    private void LoadScores() {
        GetScoreGameObjects();
        for (int i = 0; i < scoreGameObjects.Count; i++) {
            clearedScoreGameObjects.Add(scoreGameObjects[i]);
        }
        scoreGameObjects = GetHighScores(scoreGameObjects);
    }

    /**
      *
      * Helper method to find the GameObjects designated as 
      * High Score game objects.
      *
      * This method takes the array of GameObjects with 
      * the tag HighScoreGameObject and inserts them into
      * the scoreGameObjects list in the appropriate order.
      * 
      */
    private void GetScoreGameObjects() {
        scoreGameObjects = new List<GameObject>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("HighScoreGameObject");
        string name;
        int index;
        foreach (GameObject obj in objs) {
            name = obj.name;
            index = int.Parse(name.Split(" ")[name.Split(" ").Length - 1]) - 1;
            scoreGameObjects.Insert(index, obj);
        }
    }

    /**
      *
      * Helper method to populate the TextMeshPro elements in a
      * GameObject that displays the score information.
      *
      * This method takes a HighScore data object and a GameObject
      * that displays the score information and populates the
      * high score information in the GameObject.
      *
      * @param score - a HighScore data object containing a high
      *                 score value and the player's name
      * @param scoreObj - a GameObject that displays the high
      *                 score information
      *
      * @return a GameObject with the TextMeshPro elements named
      *                 Name and Score populated based on the
      *                 HighScore data object's information
      *
      */
    private static GameObject GetHighScoreGameObject(HighScore score, GameObject scoreObj) {
        GameObject newScoreObj = GameObject.Instantiate(scoreObj, scoreObj.transform.position, Quaternion.identity, scoreObj.transform.parent.transform);
        scoreObj.SetActive(false);
        newScoreObj.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = score.name;
        // ToString.("n0") formats the value to a number with 0 decimal places
        newScoreObj.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = score.score.ToString("n0");
        return newScoreObj;
    }
}