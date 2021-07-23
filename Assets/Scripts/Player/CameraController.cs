using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera Camera1, Camera2;


    public bool MultiplayerMode;

    private void Start()
    {
        

    }
    
    public void Update()
    {
        GameManager.instance.players[0].gameObject.name = "Player One";
        Camera1 = GameManager.instance.players[0].gameObject.GetComponent<Camera>();
        //Camera2 = GameManager.instance.players[1].GetComponent<Camera>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            MultiplayerMode = !MultiplayerMode;
            SetSplitScreen();
        }

    }

    public void SetSplitScreen()
    {
        if(MultiplayerMode)
        {
            Camera1.rect = new Rect(0f, .5f, 1f, .5f);
            Camera2.rect = new Rect(0f, 0f, 1f, .5f);
        }
        else
        {
            Camera1.rect = new Rect(0f, 0f, .5f, 1f);
            Camera2.rect = new Rect(.5f, 0f, .5f, 1f);
        }

    }




}
