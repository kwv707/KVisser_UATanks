using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<PlayerController> players;
    public List<SpawnPoint> SpawnLocations;
    public List<Scores> ScoreData;
    public Room[,] grid;
    public GameObject PlayerOne;
    public GameObject SinglePlayer;
    public GameObject PlayerTwo;
    private bool PlayerOneSpawned = false;
    private bool PlayerTwoSpawned = false;

    [HideInInspector] public bool isGamePaused = false;
     public bool isSinglePlayerMode = true;



    void Awake()
    {
        overLordManager();

        
        
    }

    public void Start()
    {

        
        StartCoroutine(Delay());
        
    }

    private void Update()
    {
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        GameModeType();
    }



    private void overLordManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There can only be one GameManager.");
            Destroy(gameObject);
        }
    }

    public void SpawnSinglePlayer()
    {


        // grabs a random spawn location and places it into spawn
        int spawn = Random.Range(0, SpawnLocations.Count);

        // places our character at the random spawn location selected randomly
        GameObject.Instantiate(SinglePlayer, SpawnLocations[spawn].transform.position, Quaternion.identity);


    }

    public void SpawnPlayerOne()
    {


        // grabs a random spawn location and places it into spawn
        int spawn = Random.Range(0, SpawnLocations.Count);

        // places our character at the random spawn location selected randomly
        GameObject.Instantiate(PlayerOne, SpawnLocations[spawn].transform.position, Quaternion.identity);


    }
    public void SpawnPlayerTwo()
    {


        // grabs a random spawn location and places it into spawn
        int spawn = Random.Range(0, SpawnLocations.Count);

        // places our character at the random spawn location selected randomly
        GameObject.Instantiate(PlayerTwo, SpawnLocations[spawn].transform.position, Quaternion.identity);


    }

    private void GameModeType()
    {

        if (isSinglePlayerMode == true)
        {

            if (PlayerOneSpawned == false)
            {
                SpawnSinglePlayer();
                GameManager.instance.players[0].GetComponent<PlayerController>().name = "Player One";
                GameManager.instance.players[0].GetComponentInChildren<Camera>().name = "Player One Cam";
                PlayerOneSpawned = true;

            }

        }
        if (isSinglePlayerMode == false)
        {




            if (PlayerOneSpawned == false)
            {
                SpawnPlayerOne();
                PlayerOneSpawned = true;

            }



            if (PlayerTwoSpawned == false)
            {
                SpawnPlayerTwo();
                PlayerTwoSpawned = true;

            }



        }

    }



}
