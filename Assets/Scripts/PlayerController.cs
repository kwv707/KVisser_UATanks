using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;

    public TankData data;
    
    float  haxis;

    public float gravity = 3.0f;


    Vector3 moveDirection;
    Vector3 rotateVector;


    public void OnMove(InputAction.CallbackContext context)
    {
        //mMove = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //mLook = context.ReadValue<Vector2>();
    }



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
    }


    private void Move()
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


    private void Rotate()
    {
        float lookAround = Input.GetAxis("Mouse X") * data.turnSpeed * Time.deltaTime;
        float lookUpDown = Input.GetAxis("Mouse Y") * data.turnSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, lookAround);
        transform.Rotate(new Vector3(Mathf.Clamp(lookUpDown, 45 , 45), 0, 0));

    }





}
