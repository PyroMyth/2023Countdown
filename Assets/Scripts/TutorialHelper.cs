using UnityEngine;
using System.Collections.Generic;

public class TutorialHelper : Grabbable {
    private Dictionary<string, string> gameObjectToArrow = new Dictionary<string, string> {
        {"BottleOfWater", "CollectWaterArrow"},
        {"Brick1", "CollectBrickArrow"}
    };

    new public void ExecuteGrab() {
        Debug.Log(gameObject.name + " has been grabbed in the tutorial!");
        ApplyPoints();
        Destroy(gameObject);
        if (gameObjectToArrow.ContainsKey(gameObject.name)) {
            Destroy(GameObject.Find(gameObjectToArrow[gameObject.name]));
        }
    }
}
