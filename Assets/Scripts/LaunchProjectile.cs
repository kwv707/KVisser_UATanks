using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchProjectile : MonoBehaviour
{

    public TankData data;
    public Camera cam;
    public GameObject projectile;
    public Transform firepoint;
    
    
    private float timeToFire;

    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -1.0F, 0);

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

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
            destination = ray.GetPoint(3000);

        InstantiateProjectile();

    }

    void InstantiateProjectile()
    {

        var projectileObject = Instantiate(projectile, firepoint.position, Quaternion.identity) as GameObject;
        projectileObject.GetComponent<Rigidbody>().velocity = (destination - firepoint.position).normalized * data.ProjectileSpeed;

    }



}
