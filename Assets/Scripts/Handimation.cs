using UnityEngine;
using UnityEngine.InputSystem;

public class Handimation : MonoBehaviour {

    // GameObjects for each hand image
    GameObject open_hand;
    GameObject closed_hand;

    void Start() {
        // grab objs of the two hands
        open_hand = transform.Find("Open").gameObject;
        closed_hand = transform.Find("Grab").gameObject;
        // Start with the hand open
        OpenHand();
    }

    // Update is called once per frame
    void Update() {
        // Check if the Mouse button is pressed
        if (Mouse.current.leftButton.isPressed) {
            // Close the hand
            CloseHand();
        } else {
            // Open the hand
            OpenHand();
        }
    }

    private void OpenHand() {
        open_hand.SetActive(true);
        closed_hand.SetActive(false);
    }

    private void CloseHand() {
        open_hand.SetActive(false);
        closed_hand.SetActive(true);
    }
}
