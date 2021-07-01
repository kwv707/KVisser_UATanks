using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchProjectile : MonoBehaviour
{

    public TankData data;
    public Camera cam;
    public Transform firepoint;
    
    
    private float timeToFire;



    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / data.FireRate;
            fireRound();
        }
    }



    void fireRound()
    {

        

        InstantiateProjectile();

    }

    void InstantiateProjectile()
    {

        data.projectile = Instantiate(data.projectile, firepoint.position, Quaternion.identity);
        data.projectile.AddForce(transform.up,ForceMode.Force);


    }



}
