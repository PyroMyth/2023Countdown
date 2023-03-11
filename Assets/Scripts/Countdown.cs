using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Countdown : MonoBehaviour {
    [Header("Countdown Config")]
    public float counterSeconds = 120f;

    [Header("Warning Pulse Config")]
    public float minAlpha = 96/256f;
    public float maxAlpha = 196/256f;
    public float warningStartTime = 20f;
    public float pulseSpeedSeconds = 1f;

    private TextMeshProUGUI counterLabel;
    private TextMeshProUGUI counterValue;
    private GameObject warningImage;
    private Graphic warningPulse;
    private float counter;
    private bool counterRunning;

    // Start is called before the first frame update
    void Start() {
        counter = counterSeconds;
        counterRunning = true;
        counterLabel = transform.Find("Countdown Label").GetComponent<TextMeshProUGUI>();
        counterValue = transform.Find("Countdown Value").GetComponent<TextMeshProUGUI>();
        warningPulse = transform.Find("Warning Pulse").GetComponent<Graphic>();
        //warningImage = transform.Find("Warning Pulse");
        //warningImage.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (counterRunning) {
            counter -= Time.deltaTime;
            if (counter <= warningStartTime) {
                StartCoroutine(WarningPulse());
            }
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

    private IEnumerator WarningPulse() {
        Color changeColor = warningPulse.color;
        //warningImage.SetActive(true);
        while (counterRunning) {
            changeColor.a = minAlpha;
            warningPulse.color = changeColor;
            //yield return new WaitForSeconds(pulseSpeedSeconds);
            // for (int i = 0; i < 5; i++) {
            while (changeColor.a <= maxAlpha) {
                Debug.Log("alpha: " + changeColor.a);
                new WaitForSeconds(pulseSpeedSeconds);
                //yield return new WaitForSeconds(pulseSpeedSeconds);
                changeColor.a += 5/255;
                warningPulse.color = changeColor;
            }

        }
        yield return null;
    }
}
