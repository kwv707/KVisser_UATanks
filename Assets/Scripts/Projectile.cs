using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag != "Bullet" && collider.gameObject.tag != "Player" )
        {
                        
            Destroy(gameObject);
            
        }



    }



}
