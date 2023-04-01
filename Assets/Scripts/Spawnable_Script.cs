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

    public int spawnable_object_amount = 3;
    // control how many trash objs we want
    public int trash_spawn_count = 0;
    // control how many belonging objs we want
    public int belonging_spawn_count = 0;





    void Start()
    {
        SpawnRandom();
 
    }

    public void SpawnRandom()
    {

        // trash --> loop 4 times
        for (int i = 0; i <= spawnable_object_amount; i++)
        {
            // find the ground
            GameObject trash_instantiate_target = GameObject.FindWithTag("Ground");
            // grab a random position
            SpawnPos = new Vector3(Random.Range(-10f, 10), Random.Range(0, 1), Random.Range(-10f, 10f));
            // grab a random rotation
            RotatePos = new Quaternion(Random.Range(0f, 100f), 0, 0, 0);

            if (trash_instantiate_target != null) // If the object exists
            {
                // control amount of spawned objs
                while (trash_spawn_count < 5)
                {
                    // choose a random belonging object
                    GameObject toSpawnTrash = trash_prefabs[Random.Range(0, trash_prefabs.Length)];
                    // declare variable of instantiated prefab
                    GameObject spawnedTrash = Instantiate(toSpawnTrash, SpawnPos, RotatePos);
                    // spawn the prefab on the table
                    spawnedTrash.transform.position = trash_instantiate_target.transform.position; // Set the spawned object's parent to the target object
                                                                                                            // add 1 to spawn amount
                    trash_spawn_count++;
                    Debug.Log(trash_spawn_count);
                }
            }

        }

        // belongings --> loop 4 times
        for (int i = 0; i <= spawnable_object_amount; i++)
        {
            
            // find table
            GameObject belongings_instantiate_target = GameObject.FindWithTag("Table");
            // grab random position
            SpawnPos = new Vector3(Random.Range(-10f, 10), Random.Range(0, 1), Random.Range(-10f, 10f));
            // grab random rotation
            RotatePos = new Quaternion(Random.Range(0f, 100f), 0, 0, 0);

            if (belongings_instantiate_target != null) // If the object exists
            {
                // control amount of spawned objs
                while (belonging_spawn_count < 5)
                {
                    // choose a random belonging object
                    GameObject toSpawnBelonging = belongings_prefabs[Random.Range(0, trash_prefabs.Length)];
                    // declare variable of instantiated prefab
                    GameObject spawnedBelonging = Instantiate(toSpawnBelonging, SpawnPos, RotatePos);
                    // spawn the prefab on the table
                    spawnedBelonging.transform.position = belongings_instantiate_target.transform.position; // Set the spawned object's parent to the target object
                                                                                                            // add 1 to spawn amount
                    belonging_spawn_count++;
                    Debug.Log(belonging_spawn_count);
                }
            }            
        }
    }
}