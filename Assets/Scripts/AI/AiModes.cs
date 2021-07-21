using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiModes : MonoBehaviour
{
    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest };
    public AIState aiState = AIState.Chase;
    public float stateEnterTime;
    public float aiSenseRadius;
    public float restingHealRate; // in hp/second
    private float timeToFire;
    private int avoidanceStage = 0;
    private float exitTime;
    public Transform target;
    public Transform Aitrans;
    public AiMotor AiMotor;
    public AiData data;


    public void Start()
    {
        Aitrans = gameObject.GetComponent<Transform>();
    }

    public void CheckForFlee()
    {
        if(data.Health < 15)
        {
            Fleeing();
        }
    }

    public void DoRest()
    {
        // Increase our health. Remember that our increase is "per second"!
        data.Health += restingHealRate * Time.deltaTime;

        // But never go over our max health
        data.Health = Mathf.Min(data.Health, data.MaxHealth);
    }

    public void ChangeState(AIState newState)
    {
        // Change our state
        aiState = newState;

        // save the time we changed states
        stateEnterTime = Time.time;
    }

    public void Chasing()
    {
        print("I'm chasing");
        AiMotor.RotateTowards(target.position, data.turnSpeed);



        //If the distance between myself and the target is greater than X
        if ((Vector3.Distance(target.transform.position, Aitrans.position) > 1))
        {
            //Then you can move forward
            AiMotor.Move(data.moveSpeed);

        } //Otherwise it won't call move


    }
    void Fleeing()
    {
        Vector3 DistToTargetVector = -1 * (target.position - Aitrans.position);

        DistToTargetVector.Normalize();

        Vector3 fleePosition = DistToTargetVector * data.fleeDist + Aitrans.position;
        AiMotor.RotateTowards(fleePosition, data.turnSpeed);
        AiMotor.Move(data.moveSpeed);
    }



    void DoAvoidance()
    {
        if (avoidanceStage == 1)
        {
            // Rotate left
            AiMotor.Rotate(-1 * data.turnSpeed);

            // If I can now move forward, move to stage 2!
            if (CanMove(data.moveSpeed))
            {
                avoidanceStage = 2;

                // Set the number of seconds we will stay in Stage2
                exitTime = data.avoidanceTime;
            }

            // Otherwise, we'll do this again next turn!
        }
        else if (avoidanceStage == 2)
        {

            if (CanMove(data.moveSpeed))
            {
                // Subtract from our timer and move
                exitTime -= Time.deltaTime;
                AiMotor.Move(data.moveSpeed);

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
        // otherwise, we can move, so return true
        return true;
    }

    void StateChange(AIState newState)
    {

        // Change our state
        aiState = newState;

        // save the time we changed states
        data.stateEnterTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {

        if (aiState == AIState.Chase)
        {
            //Perform Behaviors
             if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                Chasing();
            }

            // Check for Transitions
            if (data.Health < data.MaxHealth * 0.5f)
            {
                ChangeState(AIState.CheckForFlee);
            }
            else if (Vector3.Distance(target.position, Aitrans.position) <= aiSenseRadius)
            {
                ChangeState(AIState.ChaseAndFire);
            }
        }
        else if (aiState == AIState.ChaseAndFire)
        {
            //Perform Behaviors
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                Chasing();

                // Limit our firing rate, so we can only shoot if enough time has passed
                if (Time.time >= timeToFire)
                {
                    AiMotor.fireRound(); // Note: This assumes we have a "shooter" component with a "Shoot()" function
                    timeToFire = Time.time + 1 / data.FireRate;
                }

                // Check for Transitions
                if (data.Health < data.MaxHealth * 0.5f)
                {
                    ChangeState(AIState.CheckForFlee);
                }
                else if (Vector3.Distance(target.position, Aitrans.position) > aiSenseRadius)
                {
                    ChangeState(AIState.Chase);
                }
            }
        }
        else if (aiState == AIState.Flee)
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
            if (Time.time >= data.stateEnterTime + 30)
            {
                StateChange(AIState.CheckForFlee);
            }
        }

        else if (aiState == AIState.Rest)
        {
            // Perform Behaviors
            DoRest();

            // Check for Transitions
            if (Vector3.Distance(target.position, Aitrans.position) <= aiSenseRadius)
            {
                ChangeState(AIState.Flee);
            }
            else if (data.Health >= data.MaxHealth)
            {
                ChangeState(AIState.Chase);
            }
        }

    }
    
}
