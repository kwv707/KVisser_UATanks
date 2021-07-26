using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Camera Camera1, Camera2;
    public static SpawnPoint instance;

    public bool PlayerOneSpawnZone = false;
    public bool PlayerTwoSpawnZone = false;
    private bool PlayerOneSpawned = false;
    private bool PlayerTwoSpawned = false;
    public bool MultiplayerMode = false;
    public bool SinglePlayer = false;

    // Start is called before the first frame update
    public void Awake()
    {
         
    }

    private void Start()
    {
        GameManager.instance.SpawnLocations.Add(this);
        
       // StartCoroutine(Delay());
       // //StartCoroutine(POneChangeName());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        GameModeType();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameModeType()
    {
        
        if(SinglePlayer == true)
        {    
            
                if (PlayerOneSpawned == false)
                {
                    SpawnPlayerOne();
                    GameManager.instance.players[0].GetComponent<PlayerController>().name = "Player One";
                    GameManager.instance.players[0].GetComponentInChildren<Camera>().name = "Player One Cam";
                    PlayerOneSpawned = true;

                }
            
        }
        if(MultiplayerMode == true)
        {
            
            
           
                
                if(PlayerOneSpawned == false)
                {
                    
                    
                    SpawnPlayerOne();

                    GameManager.instance.players[0].GetComponent<PlayerController>().name = "Player One";
                    GameManager.instance.players[0].GetComponentInChildren<Camera>().name = "Player One Cam";
                    PlayerOneSpawned = true;

                }                
           
           
           
                if (PlayerTwoSpawned == false)
                {
                    SpawnPlayerTwo();
                    GameManager.instance.players[1].name = "Player Two";
                    GameManager.instance.players[1].GetComponent<Camera>().name = "Player Two Cam";
                    GameManager.instance.players[1].GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, .5f);
                    PlayerTwoSpawned = true;

                }
                

           
        }
        
    }
    
    private void SpawnPlayerOne()
    {
        //// grabs a random spawn location and places it into spawn
        int spawn = Random.Range(0, GameManager.instance.SpawnLocations.Count);

        //// places our character at the random spawn location selected randomly
        GameObject.Instantiate(GameManager.instance.PlayerOne, GameManager.instance.SpawnLocations[spawn].transform.position, Quaternion.identity);

        //GameManager.instance.players[0].GetComponent<GameObject>().GetComponent<Camera>().rect = new Rect(0f, .5f, 1f, .5f);
        
    }

    private void SpawnPlayerTwo()
    {
        // grabs a random spawn location and places it into spawn
        int spawn = Random.Range(0, GameManager.instance.SpawnLocations.Count);

        // places our character at the random spawn location selected randomly
        GameObject.Instantiate(GameManager.instance.PlayerTwo, GameManager.instance.SpawnLocations[spawn].transform.position, Quaternion.identity);
        

    }



}
