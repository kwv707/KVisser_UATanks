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
        GameManager.instance.players.Add(this);
        CharacterController = GetComponent<CharacterController>();
        Physics.gravity = new Vector3(0, -1.0F, 0);
        
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnDestroy()
    {
       GameManager.instance.players.Remove(this);
       // GameManager.instance.ScoreData.Add(data.PlayerScore);
    }

    public PlayerController getNearestPLayer()
    {
        PlayerController nearestPlayer = GameManager.instance.players[0];

        float playerDistance = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);

        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {

            if (Vector3.Distance(transform.position, GameManager.instance.players[i].transform.position) < playerDistance)
            {
                nearestPlayer = GameManager.instance.players[i];
                playerDistance = Vector3.Distance(transform.position, GameManager.instance.players[i].transform.position);
            }

        }
        return nearestPlayer;

    }


    // Update is called once per frame
    void Update()
    {
        
        Move();
        

        if (Input.GetKey(KeyCode.D))
        {
            Rotate(data.turnSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(-data.turnSpeed);
        }

        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / data.fireRate;
            fireRound();
        }
    }


    void Move()
    {
        float moveforward = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveforward); 
            moveDirection *= data.moveSpeed;
            moveDirection = transform.TransformDirection(moveDirection);        

        CharacterController.Move(moveDirection * Time.deltaTime);

        Vector3 gravityVector = Vector3.zero;

        if (!CharacterController.isGrounded)
        {
            gravityVector.y -= gravity;
        }

        CharacterController.Move(gravityVector * Time.deltaTime);

    }


    void Rotate(float turnSpeed)
    {
                
        Vector3 rotateVector = Vector3.up * turnSpeed * Time.deltaTime;

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
