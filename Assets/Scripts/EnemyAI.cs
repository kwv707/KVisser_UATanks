using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
          if (gameObject.tag == "Big Boy")
          {
                bigBoy();            
          }  
            Destroy(gameObject);
        }
    }


    void bigBoy()
    {
        
    }




}