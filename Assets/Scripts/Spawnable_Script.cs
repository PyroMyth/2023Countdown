using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable_Script : MonoBehaviour
{
    // variables that grab array of trash and belongings
    public GameObject[] trash_prefabs;
    
    public GameObject[] belongings_prefabs;

    // delcare two variables of the min and max distance gameobjects can spawn
    private Vector3 SpawnPos;
    private Quaternion RotatePos;





    void Start()
    {
        SpawnRandom("Table", 10);
        SpawnRandom("Ground", 10);

    }

    /*spawns objects in a random position
    -it accepts a string that is the target for the object to instantiate on
    -a spawn count that controls how many objects can be spawned in the scene
    -the spawn object, that grabs a random object from the list of trash/belongings
    -a gameobject array of either trash or belongings
    */

    public void SpawnRandom(string target, int spawn_count)
    {

        // trash --> loop 4 times
        for (int i = 0; i <= spawn_count; i++)
        {
            // find the ground
            GameObject instantiate_target = GameObject.FindGameObjectWithTag(target);
            // grab a random position
            SpawnPos = new Vector3(Random.Range(-10f, 10), Random.Range(0, 1), Random.Range(-10f, 10f));
            // grab a random rotation
            RotatePos = new Quaternion(Random.Range(0f, 100f), 0, 0, 0);

            if (instantiate_target != null) // If the object exists
            {
                // choose a random belonging object
                GameObject spawnObject = trash_prefabs[Random.Range(0, trash_prefabs.Length)];
                // declare variable of instantiated prefab
                GameObject instantiateObject = Instantiate(spawnObject, SpawnPos, RotatePos);
                // spawn the prefab on the table
                spawnObject.transform.position = instantiate_target.transform.position; // Set the spawned object's parent to the target object
            }
        }
    }
}