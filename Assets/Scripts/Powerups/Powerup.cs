using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Powerup 
{
    // Checks
    public bool isPermanent;

    // Modifiers
    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;
    public float fireRateModifier;
    public float healthBoost;
    public float duration = 15f;



    public void OnActivate(TankData target)
    {
        target.moveSpeed += speedModifier;
        target.health += healthModifier;
        target.maxHealth += maxHealthModifier;
        target.fireRate += fireRateModifier;
    }

    public void OnDeactivate(TankData target)
    {
        target.moveSpeed -= speedModifier;
        target.health -= healthModifier;
        target.maxHealth -= maxHealthModifier;
        target.fireRate -= fireRateModifier;
    }




}
