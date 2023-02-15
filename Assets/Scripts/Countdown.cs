using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour {
    [Header("Countdown Config")]
    public float counterSeconds = 120f;

    private TextMeshProUGUI counterLabel;
    private TextMeshProUGUI counterValue;
    private float counter;
    private bool counterRunning;

    // Start is called before the first frame update
    void Start() {
        counter = counterSeconds;
        counterRunning = true;
        counterLabel = transform.Find("Countdown Label").GetComponent<TextMeshProUGUI>();
        counterValue = transform.Find("Countdown Value").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (counterRunning) {
            counter -= Time.deltaTime;
            if (counter <= 0) {
                counter = 0;
                counterRunning = false;
            }
            SetCounter(counter);
        }
    }

    private void SetCounter(float timeLeft) {
        counterValue.text = ((int)timeLeft).ToString() + " seconds left!";
    }
}
