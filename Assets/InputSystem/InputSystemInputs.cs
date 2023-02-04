using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemInputs : MonoBehaviour {
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    [Header("Grab Settings")]
    public float maxDistance = 10f;

    private Camera mainCamera;

    public void Start() {
        mainCamera = Camera.main;
    }

    public void OnMove(InputValue value) {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value) {
        if (cursorInputForLook) {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnSprint(InputValue value) {
        SprintInput(value.isPressed);
    }

    public void OnGrab(InputValue value) {
        // Debug.Log("In OnGrab");
        //Debug.Log(Camera.main.ScreenToWorldPoint(Mouse.current.position));
        // Debug.Log(Mouse.current.position.x.ReadValue());
        // Debug.Log(Mouse.current.position.y.ReadValue());
        GameObject crossHand = GameObject.Find("CrossHand");
        GrabInput(crossHand.transform.position);
    }

    public void MoveInput(Vector2 newMoveDirection) {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection) {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState) {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState) {
        sprint = newSprintState;
    }

    public void GrabInput(Vector3 grabDirection) {
        Ray grabRay = Camera.main.ScreenPointToRay(grabDirection);
        RaycastHit[] hits = Physics.RaycastAll(grabRay, maxDistance);
        //Debug.DrawRay(grabRay.origin, grabRay.direction, Color.red, 8.0f);
        RaycastHit hit;
        Grabbable grabbed;
        for (int i = 0; i < hits.Length; i++) {
            hit = hits[i];
            Debug.Log("Hit #" + i + " = " + hit.transform.gameObject.name);
            grabbed = hit.transform.gameObject.GetComponent<Grabbable>();
            if (grabbed != null) {
                grabbed.ExecuteGrab();
                break;
            }
            Debug.Log("Goodbye!");
        }
    }

    private void OnApplicationFocus(bool hasFocus) {
        //SetCursorState(cursorLocked);
        // Set cursor visibility
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		Cursor.visible = true;
    }

    private void SetCursorState(bool newState) {
        //Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        // Set cursor visibility
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		Cursor.visible = true;
    }
}
