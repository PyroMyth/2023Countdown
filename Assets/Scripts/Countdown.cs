using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Countdown : MonoBehaviour {
    [Header("Countdown Config")]
    public float counterSeconds = 120f;

    [Header("Warning Pulse Config")]
    public float minAlpha = 0.35f;
    public float maxAlpha = 0.85f;
    public float alphaStep = 0.02f;
    public float warningStartTime = 20f;
    public float pulseSpeedSeconds = 0.2f;
    public float pulseSpeedReductionMultiplier = 0.2f;

    // Text Labels
    private TextMeshProUGUI counterLabel;
    private TextMeshProUGUI counterValue;

    // Counter Variables
    private float counter;
    private bool counterRunning;

    // Warning Pulse Variables
    private GameObject warningImage;
    private Graphic warningPulse;
    private Color pulseColor;
    private float pulseTimer;
    private bool isAdding;

    // Start is called before the first frame update
    void Start() {
        // Initialize Counter
        counter = counterSeconds;
        counterRunning = true;

        // Initialize Labels
        counterLabel = transform.Find("Countdown Label").GetComponent<TextMeshProUGUI>();
        counterValue = transform.Find("Countdown Value").GetComponent<TextMeshProUGUI>();

        // Initialize Warning Pulse
        warningPulse = transform.Find("Warning Pulse").GetComponent<Graphic>();
        pulseColor = warningPulse.color;
        pulseColor.a = minAlpha;
        pulseTimer = 0f;
        isAdding = true;
    }

    public void Pause() {
        counterRunning = false;
    }

    public void Restart() {
        counterRunning = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        // When the counter is running
        if (counterRunning) {
            // Decrement the counter
            counter -= Time.deltaTime;
            // If it's time for the warning pulse
            if (counter <= warningStartTime) {
                // Only run the coroutine if enough time has passed
                if (pulseTimer > pulseSpeedSeconds) {
                    pulseTimer = 0f;
                    StartCoroutine(WarningPulse());
                } else {
                    // Increment the pulse timer
                    pulseTimer += Time.deltaTime;
                }
            }
            // If time is up
            if (counter <= 0) {
                // Stop the counter
                counter = 0;
                counterRunning = false;
            }
            // Run the code to update the counter value
            SetCounter(counter);
        }
    }


    // Update the text of the Counter
    private void SetCounter(float timeLeft) {
        counterValue.text = ((int)timeLeft).ToString() + " seconds left!";
    }

    // Coroutine to pulse the warning image
    private IEnumerator WarningPulse() {
        // If the counter is running
        if (counterRunning) {
            // If we are increasing the alpha
            if (isAdding) {
                pulseColor.a += alphaStep;
            } else { // Otherwise, we are decreasing the alpha
                pulseColor.a -= alphaStep;
            }
            // If alpha is too high
            if (pulseColor.a > maxAlpha) {
                // Toggle the isAdding flag, switch to decrementing alpha
                isAdding = false;
            } else if (pulseColor.a < minAlpha) { // If alpha is too low
                // Toggle the isAdding flag, switch to incrementing alpha
                isAdding = true;
                // After a full cycle of adding and subtracting,
                // Reduce the pulse speed to make it pulse faster
                pulseSpeedSeconds -= pulseSpeedReductionMultiplier * pulseSpeedSeconds;
            }
            // Update the warning pulse color (only alpha has changed)
            warningPulse.color = pulseColor;
        } else { // If the counter is not running
            // Set the alpha to full opaque and update the warning pulse color
            pulseColor.a = 1;
            warningPulse.color = pulseColor;
        }
        // Get out of the coroutine
        yield return null;
    }
}
