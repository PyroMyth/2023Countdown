using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable_Script : MonoBehaviour
{
    // grab game objects trash, belongings

    public GameObject trash;
    public GameObject belonging;

    // check trash and belongings objects with a tag

    // create tags around groundable objects and spawn the objects on the ground

    void Start()
    {
        // find ground
        GameObject spawnPoint = GameObject.FindWithTag("Ground");

        // if spawn-point is somewhere in the game --> spawn
        if (spawnPoint != null)
        {
            // spawn game object!
            Instantiate(trash, spawnPoint.transform.position + new Vector3(0,10,0), Quaternion.identity);

            Instantiate(belonging, spawnPoint.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        }
    }
}
