using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelWithButtons : MonoBehaviour {
    private static GameObject[] allButtons;
    protected InputSystemInputs inputSystem;
    protected GameObject myPanel;
    protected GameObject[] myButtons;
    protected int myButtonsCount;

    public static void Init() {
        if (allButtons == null || allButtons.Length == 0) {
            allButtons = GameObject.FindGameObjectsWithTag("Button");
        }
    }

    protected string IsClicking(Vector3 mousePos) {
        RectTransform buttonRect;
        Vector3 origMousePos = mousePos;
        Debug.Log("MousePosition: " + mousePos.x + "," + mousePos.y);
        
        // Expecting pivot positions for all buttons to be at 0,0 local coords
        Vector3[] corners = new Vector3[4];
        for (int i = 0; i < myButtons.Length; i++) {
            if (myButtons[i] != null) {
                buttonRect = myButtons[i].GetComponent<RectTransform>();
                buttonRect.GetWorldCorners(corners);
                Debug.Log("Button Corners:" + corners[0] + "," + corners[1] + "," + corners[2] + "," + corners[3]);
                if (mousePos.x >= corners[0].x && 
                    mousePos.x <= corners[2].x &&
                    mousePos.y >= corners[0].y && 
                    mousePos.y <= corners[1].y) {
                    Debug.Log("Found it!");
                    return myButtons[i].name;
                }
            }
        }
        return "";
    }

    protected void GetMyButtons() {
        int index = 0;
        myButtons = new GameObject[15];
        if (allButtons != null && myPanel != null) {
            for (int i = 0; i < allButtons.Length; i++) {
                if (allButtons[i].transform.IsChildOf(myPanel.transform)) {
                    myButtons[index] = allButtons[i];
                    index++;
                }
            }
            myButtonsCount = index + 1;
        }
    }
    public abstract void HandleClick(Vector3 mousePosition);
}
