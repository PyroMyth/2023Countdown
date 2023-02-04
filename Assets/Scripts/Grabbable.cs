using UnityEngine;

public class Grabbable : MonoBehaviour {
    public float points;

    public void ExecuteGrab() {
        Debug.Log(gameObject.name + " has been grabbed!");
        ApplyPoints();
        Destroy(gameObject);
    }

    private void ApplyPoints() {
        // TODO: add logic to apply points to player's score
    }
}
