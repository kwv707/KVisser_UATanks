using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    // Movement
    public float moveSpeed = 5f;
    public float turnSpeed = 14f;


    // Player Stats
    public float health = 100f;
    public float maxHealth = 100f;
    public float fireRate = 5f;
    public float ProjectileSpeed = 25f;
    public float projectileDamage = 15f;
    public bool heavyDamage = false;
    public float HeavyAmmoAmount = 30;


    [HideInInspector]public bool isDead = false;

    //Score System
    [HideInInspector] public int PlayerScore;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
