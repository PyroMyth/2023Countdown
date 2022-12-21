using UnityEngine;
using UnityEngine.InputSystem;

public class GrabItem : MonoBehaviour {
    
    public void OnGrab(InputValue input) {
        Debug.Log("In OnGrab");
    }
}
