using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HighScores {
    public List<HighScore> scores;

    public HighScores() {
        scores = new List<HighScore>();
    }

    public void AddScore(string name, float score) {
        AddScore(new HighScore(name, score));
    }
    public void AddScore(HighScore score) {
        scores.Add(score);
        Sort();
    }
    public void Sort() {
        scores.Sort();
        scores.Reverse();
    }
}

public class HighScore : IComparable {
    public string name;
    public float score;

    public HighScore(string n, float s) {
        name = n;
        score = s;
    }

    public HighScore() {

    }

    public int CompareTo(object other) {
        if (other == null) {
            return 1;
        }
        HighScore that = other as HighScore;
        if (that != null) {
            return this.score.CompareTo(that.score);
        } else {
            throw new ArgumentException("Compared object is not a High Score.");
        }
    }

    public override string ToString() {
        return this.name + ": " + this.score;
    }
}