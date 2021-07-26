using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{


    public float nextSpawnTime;
    public float spawnDelay;
    private Transform tf;
    public Powerup powerup;
    public AudioClip feedback;
    public GameObject pickupPrefab;
    public GameObject spawnedPickup;
    public bool isItemPickedUp;
    
    public void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;


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
                spawnedPickup = Instantiate(pickupPrefab, tf.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            // Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // variable to store other object's PowerupController - if it has one
        PowerupController powCon = other.GetComponent<PowerupController>();

        // If the other object has a PowerupController
        if (powCon != null)
        {
            // Add the powerup
            powCon.Add(powerup);

            // Play Feedback (if it is set)
            if (feedback != null)
            {
                AudioSource.PlayClipAtPoint(feedback, tf.position, 1.0f);
            }

            // Destroy this powerup
            Destroy(spawnedPickup);
        }
    }

}

