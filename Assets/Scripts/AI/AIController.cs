using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AiData;




public class AIController : MonoBehaviour
{
    CharacterController CharacterController;
    
    // Hidden in the Inspector
    //public GameObject target;
    [HideInInspector]public Projectile projectileData;
    [HideInInspector]public Transform Aitrans;
    [HideInInspector]public AiMotor AiMotor;
    [HideInInspector]public AiData AiData;

    // Data that will be allowed to touch
    public AttackMode attackMode = AttackMode.Chase;
    public Transform[] waypoints;


    
    public Transform firepoint;
    public Rigidbody projectile;
    

    

    
    private int curWay = 0;
    public float closeEnough = 1.0f;
    private int avoidanceStage = 0;
    private float exitTime;
    private float timeToFire;
    
    
    

    
    
    

    // Start is called before the first frame update
    void Awake()
    {
        
        

        CharacterController = GetComponent<CharacterController>();
        projectileData = GetComponent<Projectile>();
        AiMotor = GetComponent<AiMotor>();
        AiData = GetComponent<AiData>();
        Aitrans = transform;

               
        
    }
    public void OnDestroy()
    {
        
    }

    private void Start()
    {
       

        
    }


    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Bullet"))
        {
            AiData.Health -= 10f;
            print(AiData.Health);
        }
    }

    void Update()
    {

        


        if (AiData.Health <= 0)
        {
            Destroy(gameObject);
            
        }

        if (CanSeePlayer() == true)
        {
            print("inside attack system");
            AiSystem(attackMode);
        }
        else
        {
            AiWaypointSystem();
        }
        
        
        
    }

     void StateChange(AttackMode newState)
    {

        // Change our state
        attackMode = newState;

        // save the time we changed states
        AiData.stateEnterTime = Time.time;
    }

     void Resting()
    {

        // Increase our health. Remember that our increase is "per second"!
        AiData.Health += AiData.HealingRate * Time.deltaTime;

        // But never go over our max health
        AiData.Health = Mathf.Min(AiData.Health, AiData.MaxHealth);
    }

    public void Chasing()
    {

        print("I'm chasing");
        AiMotor.RotateTowards(Target().transform.position, AiData.turnSpeed);
        


        ////If the distance between myself and the target is greater than X
        if ((Vector3.Distance(Target().transform.position, Aitrans.position) > 1))
        {
            //Then you can move forward
            AiMotor.Move(AiData.moveSpeed);

        } //Otherwise it won't call move


    }

    void Fleeing()
    {

        Vector3 DistToTargetVector = -1 * (Target().transform.position - Aitrans.position);

        DistToTargetVector.Normalize();

        Vector3 fleePosition = DistToTargetVector * AiData.fleeDist + Aitrans.position;
        AiMotor.RotateTowards(fleePosition, AiData.turnSpeed);
        AiMotor.Move(AiData.moveSpeed);
    }

    void DoAvoidance()
    {
        if (avoidanceStage == 1)
        {
            // Rotate left
            AiMotor.Rotate(-1 * AiData.turnSpeed);

            // If I can now move forward, move to stage 2!
            if (CanMove(AiData.moveSpeed))
            {
                avoidanceStage = 2;

                // Set the number of seconds we will stay in Stage2
                exitTime = AiData.avoidanceTime;
            }

            // Otherwise, we'll do this again next turn!
        }
        else if (avoidanceStage == 2)
        {
            
            if (CanMove(AiData.moveSpeed))
            {
                // Subtract from our timer and move
                exitTime -= Time.deltaTime;
                AiMotor.Move(AiData.moveSpeed);

                // If we have moved long enough, return to chase mode
                if (exitTime <= 0)
                {
                    avoidanceStage = 0;
                }
            }
            else
            {
                // Otherwise, we can't move forward, so back to stage 1
                avoidanceStage = 1;
            }
        }
    }

    public bool CanMove(float speed)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(Aitrans.position, Aitrans.forward, out hitInfo, speed))
        {
            // ... and if what we hit is not the player...
            if (!hitInfo.collider.CompareTag("Player"))
            {
                // ... then we can't move
                return false;
            }
        }
        return true;      
    }

    void CheckForFlee()
    {
        
    }

    void AiSystem(AttackMode mode)
    {
        PlayerController nearestPlayer = GameManager.instance.players[0];

        if (GameManager.instance.players.Count > 0)
        {
            float playerDistance = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
            for (int i = 0; i < GameManager.instance.players.Count; i++)
            {
                if (Vector3.Distance(transform.position, GameManager.instance.players[i].transform.position) < playerDistance)
                {
                    nearestPlayer = GameManager.instance.players[i];
                    playerDistance = Vector3.Distance(transform.position, GameManager.instance.players[i].transform.position);
                }
            }
        }

        switch (mode)
        {            
            case AttackMode.Chase:
                {
                    // Perform Behaviors
                    if (avoidanceStage != 0)
                    {
                        Debug.Log("inside avoidance");
                        DoAvoidance();
                    }
                    else
                    {
                        Chasing();
                    }

                    // Check for Transitions
                    if (AiData.Health < AiData.MaxHealth * 0.5f)
                    {
                        StateChange(AttackMode.CheckForFlee);
                    }
                    else if (Vector3.Distance(nearestPlayer.transform.position, Aitrans.position) <= AiData.aiSenseRadius)
                    {
                        StateChange(AttackMode.ChaseAndFire);
                    }
                }
                break;

            case AttackMode.ChaseAndFire:
                {
                    // Perform actions based on interactions with the player
                    if (avoidanceStage != 0)
                    {
                        Debug.Log("inside avoidance");
                        DoAvoidance();
                    }
                    else
                    {
                        Chasing();

                        // Limiting our firing rate against the player
                        if (Time.time >= timeToFire)
                        {
                            timeToFire = Time.time + 1 / AiData.FireRate;
                            AiMotor.fireRound();
                        }
                    }
                    // Check for Transitions
                    if (AiData.Health < AiData.MaxHealth * 0.5f)
                    {
                        StateChange(AttackMode.CheckForFlee);
                    }
                    else if (Vector3.Distance(nearestPlayer.transform.position, Aitrans.position) <= AiData.aiSenseRadius)
                    {
                        StateChange(AttackMode.Chase);
                    }
                }
                break;

            case AttackMode.Flee:
                {
                    if (avoidanceStage != 0)
                    {
                        Debug.Log("inside avoidance");
                        DoAvoidance();
                    }
                    else
                    {
                        Fleeing();
                    }

                    // Check for Transitions
                    if (Time.time >= AiData.stateEnterTime + 30)
                    {
                        StateChange(AttackMode.CheckForFlee);
                    }
                }
                break;
            
            case AttackMode.CheckForFlee:
                {

                    // Perform Behaviors
                    CheckForFlee();

                    // Check for Transitions
                    if (Vector3.Distance(nearestPlayer.transform.position, Aitrans.position) <= AiData.aiSenseRadius)
                    {
                        StateChange(AttackMode.Flee);
                    }
                    else
                    {
                        StateChange(AttackMode.Rest);
                    }
                }
                break;

            case AttackMode.Rest:
                {
                    // Perform Behaviors
                    Resting();

                    // Check for Transitions
                    if (Vector3.Distance(nearestPlayer.transform.position, Aitrans.position) <= AiData.aiSenseRadius)
                    {
                        StateChange(AttackMode.Flee);
                    }
                    else if (AiData.Health >= AiData.MaxHealth)
                    {
                        StateChange(AttackMode.Chase);
                    }
                }
                break;
                
        }


    }

    void AiWaypointSystem()
    {
        if (AiMotor.RotateTowards(waypoints[curWay].position, AiData.turnSpeed))
            {
                // Empty Space
            }
            else
            {
            AiMotor.Move(AiData.moveSpeed);

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

    public PlayerController Target()
    {
        PlayerController nearestPlayer = GameManager.instance.players[0];

        if (GameManager.instance.players.Count > 0)
        {
            float playerDistance = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
            for (int i = 0; i < GameManager.instance.players.Count; i++)
            {
                if (Vector3.Distance(transform.position, GameManager.instance.players[i].transform.position) < playerDistance)
                {
                    nearestPlayer = GameManager.instance.players[i];
                    playerDistance = Vector3.Distance(transform.position, GameManager.instance.players[i].transform.position);
                }
            }
        }
        return nearestPlayer;
    }

    bool CanSeePlayer()
    {
        

        Vector3 AiToTarget = Target().transform.position - Aitrans.position;

        // Angle between the direction of the Ai
        float angleToTarget = Vector3.Angle(AiToTarget, transform.forward);

        // If angle is less than field of view
        
            RaycastHit hitInfo;


            if (Physics.Raycast(Aitrans.position, AiToTarget, out hitInfo, AiData.maxViewDistance))
            {

                // If the first object we hit is our target 
                if (Vector3.Distance(Target().transform.position, Aitrans.position) < AiData.aiSenseRadius)
                {
                    // ... and if what we hit is not the player...

                    if (hitInfo.collider.CompareTag("Wall"))
                    {
                        // ... then we can't move
                        return false;
                    }
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        return true;
                    }

                }
            }
        

        return false;
    }




}
