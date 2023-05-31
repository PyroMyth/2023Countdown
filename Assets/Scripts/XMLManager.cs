using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class XMLManager : MonoBehaviour {
    public static HighScores highScores;

    void Awake() {
        highScores = new HighScores();
        // Create the HighScores directory if it does not already exist
        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
        Debug.Log(Application.persistentDataPath);
    }

    /**
      *
      * Method to save the scores stored in the highScores variable
      * in this class
      *
      */
    public static void SaveScores() {
        SaveScores(highScores);
    }

    /**
      *
      * Method to save the scores provided to this method
      *
      * @param scoresToSave - a HighScores instance containing
      *                     the high score data to be saved
      *
      */
    public static void SaveScores(HighScores scoresToSave) {
        highScores = scoresToSave;
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/HighScores.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
        serializer.Serialize(stream, highScores.scores);
        stream.Close();
    }

    /**
      *
      * Method to load the high scores from the HighScores directory.
      *
      * This method attempts to find the file named HighScores.xml 
      * stored in the HighScores directory in the PersistentDataPath.
      *
      * The XML in the file is deserialized into a list of high scores
      * with the player's name and their score.
      *
      */
    public static HighScores LoadScores() {
        highScores = new HighScores();
        highScores.scores = new List<HighScore>();
        if (File.Exists(Application.persistentDataPath + "/HighScores/HighScores.xml")) {
            XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
            FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/HighScores.xml", FileMode.Open);
            highScores.scores = serializer.Deserialize(stream) as List<HighScore>;
        }
        highScores.Sort();
        return highScores;
    }
}
