using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/**
  *
  * This class is intended to be placed on the GameCanvas game object
  * in the game. The score information should be contained in a
  * child game object that contains the Score Label and Score Value
  * text objects.
  *
  * Keeps track of the player's score and displays the score
  * on the GameCanvas.
  *
  * Handles the end game functionality - shows the Game Over
  * panel, displays the final score and, if the score qualifies,
  * requests the player's name and saves the updated high scores
  *
  */
public class Scorekeeper : MonoBehaviour {
	// The player's score value
	private static float score;
	// The TextMeshProUGUI element that displays the score
	private static TMPro.TextMeshProUGUI scoreDisplay;
	private static GameOverMenu gameOver;
	private static GameObject timerPanel;
	private static GameObject scorePanel;

	/**
	  *
	  * Default the score value to 0
	  * Find the text object that displays the score
	  * Call the DisplayScore method to update the score display
	  *
	  */
	void Awake() {
		score = 0f;
		scoreDisplay = GameObject.Find("Score Value").transform.GetComponent<TMPro.TextMeshProUGUI>();
		Scorekeeper.DisplayScore();
		gameOver = transform.Find("Panel Game Over").transform.GetComponent<GameOverMenu>();
		timerPanel = transform.Find("Timer").gameObject;
		scorePanel = transform.Find("Score").gameObject;
	}

	/**
	  *
	  * Updates the player's score using the value provided to the method.
	  *
	  * @param value - a float containing the amount by which the player's
	  *				 score should be changed. A positive or negative
	  *				 number is valid. A positive number will increase
	  *				 the player's score and a negative number will
	  *				 decrease the player's score.
	  *
	  */
	public static void ChangeScore(float value) {
		score += value;
		DisplayScore();
	}
	
	public static void EndGame() {
        Debug.Log("score=" + score);
		gameOver.ToggleEndGame(score);
		timerPanel.SetActive(false);
		scorePanel.SetActive(false);
	}
	
	/**
	  *
	  * Updates the score display to the player on the GameCanvas
	  *
	  */
	private static void DisplayScore() {
		if (scoreDisplay != null) {
			scoreDisplay.text = score.ToString("n0");;
		}
	}
}
