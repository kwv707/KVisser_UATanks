using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIController : MonoBehaviour
{

    public AiData data;



    CharacterController CharacterController;
    
   
    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Move(float MoveSpeed)
    {
        
        Vector3 movementspeed = transform.forward * MoveSpeed;

        CharacterController.SimpleMove(movementspeed);

    }


    public void Rotate(float speed)
    {
        
        Vector3 rotateVector = Vector3.up *( speed * Time.deltaTime);

        transform.Rotate(rotateVector, Space.Self);
    }



}
