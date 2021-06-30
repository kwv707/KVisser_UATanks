using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public TankData data;
    public AIController AiController;

    public Transform[] waypoints;
    private int curWay = 0;
    public float closeEnough = 1.0f;

    GameObject enemy;


    private void Update()
    {
        

        if(RotateTowards(waypoints[curWay].position, data.turnSpeed))
        {
            // Empty Space
        }
        else
        {
            AiController.Move(5.0f);
            AiController.Rotate(3);
        }
        if (Vector3.Distance(transform.position, waypoints[curWay].position) < closeEnough)
        {
            curWay++;
        }
    }



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



    // RotateTowards (Target) - rotates towards the target (if possible).
    // If we rotate, then returns true. If we can't rotate (because we are already facing the target) return false.
    public bool RotateTowards(Vector3 target, float speed)
    {
        //Vector3 vectorToTarget;

        //// The vector to our target is the DIFFERENCE between the target position and our position.
        ////   How would our position need to be different to reach the target? "Difference" is subtraction!
        //vectorToTarget = target - tf.position;

        //// Find the Quaternion that looks down that vector
        //Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        //// If that is the direction we are already looking, we don't need to turn!
        //if (targetRotation == tf.rotation)
        //{
        //    return false;
        //}

        //// Otherwise:
        //// Change our rotation so that we are closer to our target rotation, but never turn faster than our Turn Speed
        ////   Note that we use Time.deltaTime because we want to turn in "Degrees per Second" not "Degrees per Framedraw"
        //tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, speed * Time.deltaTime);

        // We rotated, so return true
        return true;
    }



}
