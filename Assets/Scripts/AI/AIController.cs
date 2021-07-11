using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AiData;

public class AIController : MonoBehaviour
{
    CharacterController CharacterController;
    private Transform Aitrans;
    public Transform[] waypoints;
    public Transform target;
    public AttackMode attackMode;


    public AiData AiData;
    
    
    private int curWay = 0;
    public float closeEnough = 1.0f;
    
    Vector3 moveDirection;

    

    // Start is called before the first frame update
    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Aitrans = gameObject.GetComponent<Transform>();

    }


    private void Start()
    {
        Physics.gravity = new Vector3(0, -1.0F, 0);
    }
    void Update()
    {
        AiMovementSystem();
        AiWaypointSystem();
    }

   
    void AiMovementSystem()
    {

        switch(attackMode)
        {

            case AttackMode.Chase:
                RotateTowards(target.position, AiData.turnSpeed);
                Move(AiData.moveSpeed);
                break;

            case AttackMode.Flee:

                Vector3 TargetVector = target.position - Aitrans.position;

                // We can flip this vector by -1 to get a vector AWAY from our target
                Vector3 DistToTargetVector = -1 * TargetVector;

                // Now, we can normalize that vector to give it a magnitude of 1
                DistToTargetVector.Normalize();

                // A normalized vector can be multiplied by a length to make a vector of that length.
                DistToTargetVector *= AiData.fleeDist;

                // We can find the position in space we want to move to by adding our vector away from our AI to our AI's position.
                //     This gives us a point that is "that vector away" from our current position.
                Vector3 fleePosition = DistToTargetVector + Aitrans.position;
                RotateTowards(fleePosition, AiData.turnSpeed);
                Move(AiData.moveSpeed);
                break;

        }
     

    }
    
    
    
    
    void AiWaypointSystem()
    {
        if (RotateTowards(waypoints[curWay].position, AiData.turnSpeed))
            {
                // Empty Space
            }
            else
            {
                Move(AiData.moveSpeed);

            }

            if (Vector3.SqrMagnitude(waypoints[curWay].position - transform.position) < (closeEnough * closeEnough))
            {
                // Advance to the next waypoint
                switch (AiData.loopType)
                {
                    case AiData.LoopType.Stop:
                    {
                            Debug.Log("Inside Stop End patroling....");
                            break;

                    }

                    case AiData.LoopType.Loop:
                    {                                           // loops through all the waypoints in the array
                        Debug.Log("Inside Loop patroling....");
                        if (curWay < waypoints.Length - 1)
                       {
                          curWay++;
                       }
                       else
                       {
                          curWay = 0;
                       }
                       break;
                    }


                    case AiData.LoopType.PingPong:
                    {
                        Debug.Log("Inside PingPong patroling....");

                        bool isPatrolingForward = true;

                        if (isPatrolingForward)
                        {
                                // moves to the next waypoint in the Array
                            if (curWay < waypoints.Length - 1)
                            {
                                curWay++;
                            }
                            else
                            { // reverse direction and stepback one waypoint
                                    isPatrolingForward = false;
                                    curWay--;
                            }
                        }
                        else
                        {                        
                            if (curWay > 0)
                            {
                                curWay--;
                                
                            }
                            else
                            {
                                isPatrolingForward = true;
                                curWay++;
                            }
                        }

                        break;
                    }
                }

            }

        
        
    }




    public void Move(float MoveSpeed)
    {
        
        //Vector3 movementspeed = transform.forward * MoveSpeed;

        //CharacterController.SimpleMove(movementspeed);
        

        moveDirection = new Vector3(0, 0, MoveSpeed * AiData.moveSpeed);
        
        moveDirection = transform.TransformDirection(moveDirection);         

        CharacterController.Move(moveDirection * Time.deltaTime);

    }


    public void Rotate(float speed)
    {
        
        Vector3 rotateVector = Vector3.up *( speed * Time.deltaTime);

        transform.Rotate(rotateVector, Space.Self);
    }
    public bool RotateTowards(Vector3 target, float speed)
    {
        Vector3 vectorToTarget;


        vectorToTarget = target - Aitrans.position;


        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);


        if (targetRotation == Aitrans.rotation)
        {
            return false;
        }


        Aitrans.rotation = Quaternion.RotateTowards(Aitrans.rotation, targetRotation, speed * Time.deltaTime);


        return true;
    }


}
