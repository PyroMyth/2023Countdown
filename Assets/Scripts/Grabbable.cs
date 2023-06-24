using UnityEngine;

public class Grabbable : MonoBehaviour {
    public float points;
    Spawnable_Script spawnScript;

    private int total_trash_count;

    private void Awake()
    {
        spawnScript = GameObject.Find("Spawn_Manager").GetComponent<Spawnable_Script>();
        total_trash_count = spawnScript.trash_spawn_count;
    }

    public void ExecuteGrab() {
        while (total_trash_count > 0)
        {
            if (gameObject.CompareTag("Trash"))
            {
                Debug.Log(spawnScript.trash_prefabs + " has been grabbed!");
                total_trash_count -= 1;
            }

            else if (gameObject.CompareTag("Belonging"))
            {
                Debug.Log("Don't grab that");
            }
            ApplyPoints();
            Destroy(gameObject);
        }
    }

    protected void ApplyPoints() {
        // TODO: add logic to apply points to player's score
    }
}
