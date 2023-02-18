using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable_Script : MonoBehaviour
{
    // grab game objects trash, belongings

    //public GameObject trash;
    //public GameObject belonging;
    public Object[] prefabs; 

    // check trash and belongings objects with a tag

    // create tags around groundable objects and spawn the objects on the ground

void Start()
{
    prefabs = Resources.LoadAll("SpawnableObjects");
    // find ground
    GameObject spawnPointGround = GameObject.FindWithTag("Ground");
    GameObject spawnPointTable = GameObject.FindWithTag("Table");

        // if spawn-point is somewhere in the game --> spawn
        if (spawnPointGround != null && spawnPointTable != null)
        {
            //// spawn game object!
            //Instantiate(trash, spawnPointGround.transform.position + new Vector3(0,10,0), Quaternion.identity);

            //Instantiate(belonging, spawnPointTable.transform.position + new Vector3(0, 10, 0), Quaternion.identity);

            //SpawnRandom();
        }
}

//public Object SpawnRandom()
//{
//    foreach(Object x in prefabs)
//        {
//            var toSpawn = prefabs[Random.Range(0, prefabs.Length)];
//            var spawned = Instantiate(toSpawn);
//            return spawned;
//        }

//}
}