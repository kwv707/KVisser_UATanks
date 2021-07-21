using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;
    public AudioClip feedback;
    public Timer Timer;
    //public float speedIncrease = 15f;
    //public float speedDuration = 5f;



    
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
            }

            // Destroy this powerup
            Destroy(Timer.spawnedPickup);
        }
    }
}
