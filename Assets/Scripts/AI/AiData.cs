using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AiData : MonoBehaviour
{

     public float moveSpeed = 0;
     public float turnSpeed = 0;
    public float FireRate = 0;
    public float ProjectileSpeed = 0;


    public enum LoopType { Stop, Loop, PingPong };
    public LoopType loopType;

    public enum AttackMode { Chase, ChaseAndFire, CheckForFlee, Flee, Rest };
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float aiSenseRadius;
    public float stateEnterTime;
    public float HealingRate;
    public float avoidanceTime = 2.0f;
    public float fleeDist = 1.0f;
    public float fieldOfView = 45.0f;
    public float maxViewDistance = 5000f;

    public int PointsGiven;
}