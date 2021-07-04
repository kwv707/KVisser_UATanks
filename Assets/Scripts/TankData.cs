using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    // Movement
    public float moveSpeed = 5f;
    public float turnSpeed = 14f;


    // Player Stats
    private float currentHealth = 75f;
    [SerializeField] private float maxHealth = 100f;



    // Tank Stats
    public float FireRate = 50.0f;
    public float ProjectileSpeed = 25f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
