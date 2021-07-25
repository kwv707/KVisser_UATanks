using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{


    public float nextSpawnTime;
    public float spawnDelay;

    public Powerup powerup;
    public AudioClip feedback;
    public GameObject spawnedPickup;
    //public float speedIncrease = 15f;
    //public float speedDuration = 5f;


    public void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // variable to store other object's PowerupController - if it has one
        PowerupController powerPickup = other.GetComponent<PowerupController>();

        // If the other object has a PowerupController
        if (powerPickup != null)
        {
            // Add the powerup
            powerPickup.Add(powerup);

            // Play Feedback (if it is set)
            if (feedback != null)
            {
                AudioSource.PlayClipAtPoint(feedback, transform.position, 1.0f);
                nextSpawnTime = Time.time + spawnDelay;
            }

            // Destroy this powerup
            Destroy(spawnedPickup);
        }
    }


    // If it is there is nothing spawns
    public void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            print("inside pickup spawn");
            // Spawn it and set the next time
            spawnedPickup = Instantiate(spawnedPickup, transform.position, Quaternion.identity) as GameObject;
            
        }
        else
        {
            // Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
        
    }        
        
}

