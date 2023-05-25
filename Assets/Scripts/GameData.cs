using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameData : MonoBehaviour {
    private static int maxNumberOfHighScores = 6;
    private static HighScores highScores;
    private List<GameObject> scoreGameObjects;
    private List<GameObject> clearedScoreGameObjects;

// TODO: Start Remove Temporary Methods and Variables 
    public int sampleHighScore;
    public string sampleName;
    public void PopulateHighScore() {
        if (highScores == null) {
            highScores = new HighScores();
        }
        if (GameData.IsHighScore(sampleHighScore))  {
            GameData.SubmitHighScore(sampleName, sampleHighScore);
        }
    }
    public void SaveHighScores() {
        if (highScores.scores.Count > maxNumberOfHighScores) {
            HighScores temp = new HighScores();
            // highScores.scores = highScores.scores.Slice(0, maxNumberOfHighScores);
            foreach (HighScore score in highScores.scores) {
                if (temp.scores.Count < maxNumberOfHighScores) {
                    temp.AddScore(score);
                } else {
                    break;
                }
            }
            highScores = temp;
        }
        XMLManager.SaveScores(highScores);
    }
// TODO: End Remove Temporary Methods and Variables
    public void Awake() {
        clearedScoreGameObjects = new List<GameObject>();
        highScores = XMLManager.LoadScores();
        LoadScores();
        Debug.Log("Found " + highScores.scores.Count + " high scores.");
    }

    public void ClearHighScores() {
        highScores = new HighScores();
        XMLManager.SaveScores(highScores);
        for (int i = 0; i < clearedScoreGameObjects.Count; i++) {
            clearedScoreGameObjects[i].SetActive(true);
            scoreGameObjects[i].SetActive(false);
        }
    }

    private void LoadScores() {
        GetScoreGameObjects();
        for (int i = 0; i < scoreGameObjects.Count; i++) {
            clearedScoreGameObjects.Add(scoreGameObjects[i]);
        }
        scoreGameObjects = GetHighScores(scoreGameObjects);
    }

    public static bool IsHighScore(float scoreValue) {
        // Provided value is a high score if it is greater than 0 and also greater than the last high score value
        return (scoreValue > 0 && maxNumberOfHighScores > 0 && 
                    ((highScores.scores.Count < maxNumberOfHighScores) ||
                    (highScores.scores.Count == maxNumberOfHighScores && 
                    scoreValue > highScores.scores[highScores.scores.Count - 1].score)));
    }

    public static void SubmitHighScore(string gamerName, float scoreValue) {
        HighScore highScore = new HighScore(gamerName, scoreValue);
        highScores.AddScore(highScore);
    }

    public static List<GameObject> GetHighScores(List<GameObject> scoreObjects) {
        List<GameObject> retVal = new List<GameObject>();
        if (highScores.scores.Count > 0) {
            for (int i = 0; i < highScores.scores.Count; i++) {
                retVal.Add(GetHighScoreGameObject(highScores.scores[i], scoreObjects[i]));
            }
        }
        return retVal;
    }

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

    private static GameObject GetHighScoreGameObject(HighScore score, GameObject scoreObj) {
        GameObject newScoreObj = GameObject.Instantiate(scoreObj, scoreObj.transform.position, Quaternion.identity, scoreObj.transform.parent.transform);
        scoreObj.SetActive(false);
        newScoreObj.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = score.name;
        // ToString.("n0") formats the value to a number with 0 decimal places
        newScoreObj.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = score.score.ToString("n0");
        return newScoreObj;
    }
}