using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")  // 
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
        Debug.Log("hello world");
    }



}
