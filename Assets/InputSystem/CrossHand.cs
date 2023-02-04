using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CrossHand : MonoBehaviour {
    void Start() {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0f);
    }
}
