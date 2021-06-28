using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 
    public static GameManager instance;


    GameObject[] enemyAI;
    public bool totalEnemy = false;


    // Runs before any Start() functions run
    void Awake()
    {
        overLordManager();

        
        
    }


    private void Update()
    {
        enemyLocator();
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


    public void enemyLocatorRanged()
    {
        GameObject closest = null;
        float distance = 5000;
        Vector3 position = transform.position;
        foreach (GameObject go in enemyAI)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

    }

    public void enemyLocator()
    {
        
        enemyAI = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyAI.Length == 0)
        {
            Debug.Log("No Enemies Located");
        }

        if(totalEnemy == true)
        {
            
            Debug.Log(enemyAI.Length);
        }
    }

}
