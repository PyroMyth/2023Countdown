using UnityEngine;

public class UICanvasControllerInput : MonoBehaviour {

    [Header("Output")]
    public InputSystemInputs inputSystemInputs;

    public void VirtualMoveInput(Vector2 virtualMoveDirection) {
        inputSystemInputs.MoveInput(virtualMoveDirection);
    }

    public void VirtualLookInput(Vector2 virtualLookDirection) {
        inputSystemInputs.LookInput(virtualLookDirection);
    }

    public void VirtualJumpInput(bool virtualJumpState) {
        inputSystemInputs.JumpInput(virtualJumpState);
    }

    public void VirtualSprintInput(bool virtualSprintState) {
        inputSystemInputs.SprintInput(virtualSprintState);
    }
    
}