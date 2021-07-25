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
    public GameObject PlayerTwo;
    public bool isGamePaused = false;
    [HideInInspector] public bool isSinglePlayerMode = true;



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
        SpawnPlayer();
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

    public void SpawnPlayer()
    {


        // grabs a random spawn location and places it into spawn
        int spawn = Random.Range(0, SpawnLocations.Count);

        // places our character at the random spawn location selected randomly
        GameObject.Instantiate(PlayerOne, SpawnLocations[spawn].transform.position, Quaternion.identity);


    }





}
