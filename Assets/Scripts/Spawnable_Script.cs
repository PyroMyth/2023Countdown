using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable_Script : MonoBehaviour
{
    // variables that grab array of trash and belongings
    public GameObject[] trash_prefabs;
    public GameObject[] belongings_prefabs;

    [SerializeField]
    private int trash_spawn_count;
    [SerializeField]
    private int belonging_spawn_count;

    [Header("X Distance")]
    public int minDistancex;
    public int maxDistancex;

    [Header("Z Distance")]
    public int minDistancez;
    public int maxDistancez;



    // delcare two variables of the min and max distance gameobjects can spawn
    private Vector3 SpawnPos;
    private Quaternion RotatePos;

    private GameObject[] tableArray;

    void Start()
    {
        
        tableArray = GameObject.FindGameObjectsWithTag("Table");
        SpawnRandom("Table", belonging_spawn_count, belongings_prefabs);
        SpawnRandom("Ground", trash_spawn_count, trash_prefabs);

    }

    /*spawns objects in a random position
    -it accepts a string that is the target for the object to instantiate on
    -a spawn count that controls how many objects can be spawned in the scene
    -the spawn object, that grabs a random object from the list of trash/belongings
    -a gameobject array of either trash or belongings
    */

    public void SpawnRandom(string target, int spawn_count, GameObject[] spawnableGameObjsArray)
    {

        // trash --> loop 4 times
        for (int i = 0; i <= spawn_count; i++)
        {
            // find the ground
            GameObject instantiate_target = GameObject.FindGameObjectWithTag(target);
            // grab a random position
            SpawnPos = new Vector3(Random.Range(minDistancex, maxDistancex), 1, Random.Range(minDistancez, maxDistancez));
            // grab a random rotation
            RotatePos = new Quaternion(0, Random.Range(0f, 100f), 0, 0);

            if (instantiate_target != null) // If the object exists
            {
                // choose a random belonging object
                GameObject spawnObject = spawnableGameObjsArray[Random.Range(0, spawnableGameObjsArray.Length)];
                
                if (target == "Ground")
                {
                    // declare variable of instantiated prefab
                    Instantiate(spawnObject, SpawnPos, RotatePos);
                }

                else if (target == "Table")
                {
                    // set a new var of a random table in the table array
                    GameObject table = tableArray[Random.Range(0, tableArray.Length)];
                    // declare variable that grabs the table position
                    Vector3 tablePos = table.transform.position;
                    // declare variable that grabs boxcollider size on x position
                    BoxCollider tableCollider = table.GetComponent<BoxCollider>();
                    Vector3 colliderSize = tableCollider.size;
                    // declare spawn variable that randomizes the table (pos.x - (collider.x/2)), (pos.x + (collider.x/2))
                    // do each of these on the appropraite axis
                    Vector3 tableSpawnPos = new Vector3(Random.Range(tablePos.x - (colliderSize.x / 2), tablePos.x + (colliderSize.x / 2)), 1.5f, Random.Range(tablePos.z - (colliderSize.z / 2), tablePos.z + (colliderSize.z / 2)));
                    Instantiate(spawnObject, tableSpawnPos, RotatePos);
                    // set the trash object to the calculated vector3
                }
            }
        }
    }
}