using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody projectile;
    public float damageAmount = 15f;

    private void Awake()
    {

        projectile = GetComponent<Rigidbody>();
        
    }
   
    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag != "Bullet" && collider.gameObject.tag != "Wall")
        {
                        
            Destroy(gameObject);
            
        }

        if(collider.gameObject.tag == "Player")
        {

            Debug.Log("Player Hit");



        }
        if (collider.gameObject.tag == "Enemy")
        {

            Debug.Log("Enemy Hit");



        }



    }



}
