using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class InputSystemInputs : MonoBehaviour {
    // declare animation variable
    private Animation anim;

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
    private PauseMenu pauseMenu;
    private bool isPaused = false;
    private PlayerInput playerInput;

    public void Start() {
        mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        // grab the animation component from the boy
        anim = transform.Find("Boy").GetComponent<Animation>();
    }

    public void OnMove(InputValue value) {
        // if (!isPaused) {
        MoveInput(value.Get<Vector2>());
        
        // }
    }

    public void OnLook(InputValue value) {
        if (/*!isPaused && */cursorInputForLook) {
            // Debug.Log("OnLook - Raw: " + value.Get<Vector2>() + ", Viewport: " + Camera.main.ScreenToViewportPoint(value.Get<Vector2>()) + ", World: " + Camera.main.ScreenToWorldPoint(value.Get<Vector2>()));
            // GameObject crossHand = GameObject.Find("CrossHand");
            // Debug.Log("Mouse - Raw: " + crossHand.transform.position + ", Viewport: " + Camera.main.ScreenToViewportPoint(crossHand.transform.position) + ", World: " + Camera.main.ScreenToWorldPoint(crossHand.transform.position));
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnSprint(InputValue value) {
        // if (!isPaused) {
            SprintInput(value.isPressed);
        // }
    }

    public void OnGrab(InputValue value) {
        Vector3 mousePosition = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0f);
        // if (!isPaused) {
            // GameObject crossHand = GameObject.Find("CrossHand");
            // Debug.Log("Raw: " + crossHand.transform.position + ", Viewport: " + Camera.main.ScreenToViewportPoint(crossHand.transform.position) + ", World: " + Camera.main.ScreenToWorldPoint(crossHand.transform.position));
            // Cross Hand position does not get updated for some reason - using same coords as the CrossHand script that tracks the mouse movement
            GrabInput(mousePosition);
        // } else {
        //     PauseMenu.HandleClick(mousePosition);
        // }
    }

    public void OnPause(InputValue value) {
        TogglePause();
    }

    public void OnClick(InputValue value) {
        if (isPaused) {
            Vector3 mousePosition = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0f);
            Debug.Log(mousePosition);
            HandlePauseClick(mousePosition);
        }
    }

    public void MoveInput(Vector2 newMoveDirection) {
        move = newMoveDirection;
        anim.Play("Run");
        // check if player is not moving
        //if (value == null)
        //{
        //    anim.Play("Idle");
        //}
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
        Debug.DrawRay(grabRay.origin, grabRay.direction, Color.red, 4.0f);
        RaycastHit hit;
        
        // Hijack the logic for the tutorial levels
        if (SceneManager.GetActiveScene().name.Contains("Tutorial")) {
            for (int i = 0; i < hits.Length; i++) {
                hit = hits[i];
                Debug.Log("Hit #" + i + " = " + hit.transform.gameObject.name);
                TutorialHelper helper = hit.transform.gameObject.GetComponent<TutorialHelper>();
                if (helper != null) {
                    helper.ExecuteGrab();
                    Debug.Log("Goodbye from the tutorial!");
                    return;
                }
            }
        }
        Grabbable grabbed;
        for (int i = 0; i < hits.Length; i++) {
            hit = hits[i];
            Debug.Log("Hit #" + i + " = " + hit.transform.gameObject.name);
            grabbed = hit.transform.gameObject.GetComponent<Grabbable>();
            if (grabbed != null) {
                grabbed.ExecuteGrab();
                Debug.Log("Goodbye!");
                break;
            }
        }
    }

    public void TogglePause() {
        Debug.Log("In TogglePause...isPaused before toggle? " + isPaused);
        PauseMenu.Toggle(isPaused);
        isPaused = !isPaused;
        Debug.Log("In TogglePause...isPaused after toggle? " + isPaused);
        if (isPaused) {
            playerInput.SwitchCurrentActionMap("UI");
        } else {
            // Use a co-routine to switch back to the player action map
            // This way, if a pause button is on top of an item in the scene,
            // it won't be grabbed as the pause is being toggled off
            StartCoroutine(SwitchToPlayerActionMap());
        }
    }

    private IEnumerator SwitchToPlayerActionMap() {
        yield return new WaitForSecondsRealtime(0.5f);
        playerInput.SwitchCurrentActionMap("Player");
    }

    private void HandlePauseClick(Vector3 mousePosition) {
        PauseMenu.HandleClick(mousePosition);
    }

    public bool GetIsPaused() {
        return isPaused;
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
