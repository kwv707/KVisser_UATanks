using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
   

    // Start is called before the first frame update
    public void Awake()
    {
        GameManager.instance.SpawnLocations.Add(this);  
    }

    private void Start()
    {
                              // must put PLAYER SPAWN in the GAME MANAGER

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
