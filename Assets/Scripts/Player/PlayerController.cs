using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;

    public TankData data;
    
    public float gravity = 3.0f;

    private float timeToFire;
    
    public Transform firepoint;

    public Rigidbody projectile;

    public AudioSource audioSource;
    public AudioClip HeavyProjectileSound;
    public AudioClip ProjectileSound;
    public AudioClip AmmoSwitchSound;

    private bool HeavyDamageActive = false;

    public Text HeavyAmmotext;

    Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.players.Add(this);
        CharacterController = GetComponent<CharacterController>();
        Physics.gravity = new Vector3(0, -1.0F, 0);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource Null");
        }

        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnDestroy()
    {
       GameManager.instance.players.Remove(this);
       //GameManager.instance.ScoreData.Add(data.PlayerScore);
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
        if (GameManager.instance.isGamePaused == false)
        {
            Move();

            if( data.health <= 0)
            {
                Destroy(gameObject);
                
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                Rotate(data.turnSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Rotate(-data.turnSpeed);
            }
            if (Input.GetKey(KeyCode.Q))
            {

                
                    
                    audioSource.PlayOneShot(AmmoSwitchSound);
                    data.fireRate = .5f;
                    data.ProjectileSpeed = 10f;
                    data.projectileDamage = 100f;
                    data.turnSpeed = 130;
                    data.moveSpeed = 10f;
                    
                
                

            }
            if(Input.GetKey(KeyCode.E))
            {
                
                
                    
                    audioSource.PlayOneShot(AmmoSwitchSound);
                    data.fireRate = 5f;
                    data.ProjectileSpeed = 25f;
                    data.projectileDamage = 15f;
                    data.turnSpeed = 90;
                    data.moveSpeed = 7f;
                
            }
            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / data.fireRate;
                fireRound(); 
            }
            
        }         
                
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        
        if (GameManager.instance.isGamePaused == false)
        {
            if (collider.gameObject.CompareTag("Bullet"))
            {
                data.health -= Random.Range(2, 8);
                print(data.health);
            }
        }       
    }

    

    void Move()
    {
        if (GameManager.instance.isGamePaused == false)
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
            

    }


    void Rotate(float turnSpeed)
    {
        if (GameManager.instance.isGamePaused == false)
        {
            Vector3 rotateVector = Vector3.up * turnSpeed * Time.deltaTime;
            transform.Rotate(rotateVector, Space.Self);
        }          

    }
    
    void fireRound()
    {
        Rigidbody Bullet;
        
        if (GameManager.instance.isGamePaused == false)
        {
            
            
            
            if (HeavyDamageActive == true)
            {
                               
                if(data.HeavyAmmoAmount > 0)            // checks to see if heavy ammo has been activated
                {
                    Bullet = Instantiate(projectile, firepoint.position, transform.rotation);
                    Bullet.velocity = transform.TransformDirection(Vector3.forward * data.ProjectileSpeed);
                    audioSource.PlayOneShot(HeavyProjectileSound);      // plays the audio clip for the projectile
                    
                    --data.HeavyAmmoAmount;                 // reduces the amount of heavy ammo by one
                    HeavyAmmotext.text = data.HeavyAmmoAmount.ToString();           //converts the float amount for the heavy ammo to a text string
                }
                
                

            }
            else             
            {
                Bullet = Instantiate(projectile, firepoint.position, transform.rotation);
                Bullet.velocity = transform.TransformDirection(Vector3.forward * data.ProjectileSpeed);
                audioSource.PlayOneShot(ProjectileSound);                       // plays the audio clip for the projectile
            }


            Debug.Log("Pew Pew");
            
            
            

        }
            
    }


    

}
