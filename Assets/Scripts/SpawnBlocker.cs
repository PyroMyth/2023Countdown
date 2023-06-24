using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocker : MonoBehaviour
{

    public string table_check = "Table"; // assign the tag you want to check here
    public string ground_check = "Ground"; // assign the tag you want to check here

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(table_check) || other.CompareTag(ground_check))
        {
            Destroy(other.gameObject);
            Debug.Log(other.name);
        }
    }
}

