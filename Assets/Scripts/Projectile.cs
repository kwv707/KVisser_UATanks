using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" )
        {
                        
            Destroy(gameObject);
            
        }



    }



}