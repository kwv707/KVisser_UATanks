using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    TankData data;

    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float lookAround = Input.GetAxis("Mouse X") * data.turnSpeed * Time.deltaTime;

        parent.Rotate(Vector3.up, lookAround);

    }




}
