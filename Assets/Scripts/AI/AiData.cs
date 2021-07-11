using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiData : MonoBehaviour
{

     public float moveSpeed = 0;
     public float turnSpeed = 0;

    public enum LoopType { Stop, Loop, PingPong };
    public LoopType loopType;

    public enum AttackMode { Chase, Flee};
    public float fleeDist = 1.0f;



}