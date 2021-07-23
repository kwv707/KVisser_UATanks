using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public GameObject spawnedPickup;
    public float spawnDelay;
    private float nextSpawnTime;
    


    
 void Start()
    {
        
    }

    void Update()
    {
        // If it is there is nothing spawns
        if (spawnedPickup == null)
        {
            // And it is time to spawn
            if (Time.time > nextSpawnTime)
            {
                // Spawn it and set the next time
                spawnedPickup = Instantiate(spawnedPickup, transform.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
       
            else
            {
            // Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
            }
        
        }
        
    }
}
