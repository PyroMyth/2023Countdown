using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class XMLManager : MonoBehaviour {
    public static HighScores highScores;

    // Start is called before the first frame update
    void Awake() {
        highScores = new HighScores();
        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
        Debug.Log(Application.persistentDataPath);
    }

    public static void SaveScores() {
        SaveScores(highScores);
    }

    public static void SaveScores(HighScores scoresToSave) {
        highScores = scoresToSave;
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/HighScores.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
        serializer.Serialize(stream, highScores.scores);
        stream.Close();
    }

    public static HighScores LoadScores() {
        XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
        highScores = new HighScores();
        highScores.scores = new List<HighScore>();
        if (File.Exists(Application.persistentDataPath + "/HighScores/HighScores.xml")) {
            FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/HighScores.xml", FileMode.Open);
            highScores.scores = serializer.Deserialize(stream) as List<HighScore>;
        }
        highScores.Sort();
        return highScores;
    }
}
