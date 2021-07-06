using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;

    public TankData data;
    
    public float gravity = 3.0f;

    private float timeToFire;
    
    public Transform firepoint;

    public Rigidbody projectile;

    Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {

        CharacterController = GetComponent<CharacterController>();
        Physics.gravity = new Vector3(0, -1.0F, 0);
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    // Update is called once per frame
    void Update()
    {
        
        Move();
        Rotate();



        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / data.ROF_RateofFire_sec;
            fireRound();
        }
    }


    void Move()
    {
        float moveforward = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveforward); 
            moveDirection *= data.moveSpeed;
            moveDirection = transform.TransformDirection(moveDirection);        // 

        CharacterController.Move(moveDirection * Time.deltaTime);

        Vector3 gravityVector = Vector3.zero;

        if (!CharacterController.isGrounded)
        {
            gravityVector.y -= gravity;
        }

        CharacterController.Move(gravityVector * Time.deltaTime);

    }


    void Rotate()
    {
       // data.turnSpeed = Input.GetButtonDown(KeyCode.D);
        

        Vector3 rotateVector = Vector3.up * data.turnSpeed * Time.deltaTime;

        transform.Rotate(rotateVector, Space.Self);

    }

    

    void fireRound()
    {

        Debug.Log("Pew Pew");
        Rigidbody Bullet;
        Bullet = Instantiate(projectile, firepoint.position, transform.rotation);
        Bullet.velocity = transform.TransformDirection(Vector3.forward * data.ProjectileSpeed);



    }




}
