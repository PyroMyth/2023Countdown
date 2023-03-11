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

    private Vector3 tablePos;



    void Start()
    {
        //prefabs = Resources.LoadAll("SpawnableObjects");
        // find table
        GameObject belongings_prefabs = GameObject.FindWithTag("Table");
        // convert gameobject variable into vector3
        Vector3 tablePos = belongings_prefabs.transform.position;
        // if spawn-point is somewhere in the game --> spawn

        SpawnPos = new Vector3(Random.Range(-10f, 10), Random.Range(0, 1), Random.Range(-10f, 10f));
        RotatePos = new Quaternion(Random.Range(0f, 100f), 0, 0,0);

        SpawnRandom();
 
    }

    public void SpawnRandom()
    {
        
        // belongings
        foreach (GameObject x in belongings_prefabs)
        {
            for (int i = 0; i <= 10; i++)
            {
                GameObject toSpawnTrash = trash_prefabs[Random.Range(0, trash_prefabs.Length)];

                Instantiate(toSpawnTrash, SpawnPos, RotatePos);
            }
        }
        // trash
        foreach (GameObject y in trash_prefabs)
        {
            for (int i = 0; i <= 10; i++)
            {
                GameObject toSpawnTrash = trash_prefabs[Random.Range(0, trash_prefabs.Length)];

                Instantiate(toSpawnTrash, SpawnPos, RotatePos);
            }
        }
    }
}