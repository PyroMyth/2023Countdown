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
        GameObject spawnPointGround = GameObject.FindWithTag("Ground");
        GameObject spawnPointTable = GameObject.FindWithTag("Table");

        // if spawn-point is somewhere in the game --> spawn
        if (spawnPointGround != null && spawnPointTable != null)
        {
            // spawn game object!
            Instantiate(trash, spawnPointGround.transform.position + new Vector3(0,10,0), Quaternion.identity);

            Instantiate(belonging, spawnPointTable.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        }
    }
}


// take in two classes of objs: trash and belongings

// each class contains several differenct kinds of objs (belongings have cards or books)

// pick a random gameobject to spawn in that class