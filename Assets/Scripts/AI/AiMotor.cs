using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AiMotor : MonoBehaviour
{
    
    CharacterController CharacterController;
    public Transform firepoint;
    public Rigidbody projectile;
    private Transform Aitrans;
    Vector3 moveDirection;

    public AiData AiData;


    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Aitrans = transform;
    }

    public void Move(float MoveSpeed)
    {


        moveDirection = new Vector3(0, 0, MoveSpeed * AiData.moveSpeed);

        moveDirection = transform.TransformDirection(moveDirection);

        CharacterController.Move(moveDirection * Time.deltaTime);

    }

    public void fireRound()
    {
        Debug.Log("inside fireRound");

            Debug.Log("Pew Pew");
            Rigidbody Bullet;
            Bullet = Instantiate(projectile, firepoint.position, transform.rotation);
            Bullet.velocity = transform.TransformDirection(Vector3.forward * AiData.ProjectileSpeed);

    }

    public void Rotate(float speed)
    {

        Vector3 rotateVector = Vector3.up * (speed * Time.deltaTime);

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
