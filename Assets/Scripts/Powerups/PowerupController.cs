using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public List<Powerup> powerups;
    public TankData tankData;
    

    public void Start()
    {
        powerups = new List<Powerup>();

    }

    void Update()
    {
        // Loop through all the powers in the List
        foreach (Powerup power in powerups)
        {
            // Subtract from the timer
            power.duration -= Time.deltaTime;

            // If time is up, deactivate the powerup and remove it from the List
            if (power.duration <= 0)
            {
                power.OnDeactivate(tankData);
                powerups.Remove(power);
            }
        }
    }

    public void Add(Powerup powerup)
    {
        powerup.OnActivate(tankData);
        if (!powerup.isPermanent)
        {
            powerups.Add(powerup);
        }


    }
}
