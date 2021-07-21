using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[System.Serializable]
public class MapGenerator : MonoBehaviour
{

    public int rows;
    public int cols;
    
    public int mapSeed;
    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    public GameObject[] rooms;

    public bool MapOfTheDay;
    public bool RandoMapMode;

    


    // Use this for initialization
    void Start()
    {
        
        mapSeed = DateToInt(DateTime.Now.Date);

        GenerateGrid();
    }



    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public void GenerateGrid()
    {
        if (MapOfTheDay)
        {
            UnityEngine.Random.InitState(mapSeed);
        }
        if(RandoMapMode)
        {
           UnityEngine.Random.InitState(DateToInt(DateTime.Now)); 
        }
        
        

        // Clear out the grid
        GameManager.instance.grid = new Room[rows, cols];

        // For each grid row...
        for (int i = 0; i < rows; i++)
        {
            // for each column in that row
            for (int j = 0; j < cols; j++)
            {
                // Figure out the location. 
                float xPosition = roomWidth * j;
                float zPosition = roomHeight * i;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // Create a new grid at the appropriate location
                GameObject RoomCreated = Instantiate(RandomRoomGen(), newPosition, Quaternion.identity) as GameObject;

                // Set its parent
                RoomCreated.transform.parent = this.transform;

                // Give it a meaningful name
                RoomCreated.name = "Grid Section_" + j + "," + i;

                // Get the room object
                Room RoomSection = RoomCreated.GetComponent<Room>();

                // Open the doors
                // If we are on the bottom row, open the north door
                if (i == 0)
                {
                    RoomSection.doorNorth.SetActive(false);
                }
                else if (i == rows - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    RoomSection.doorSouth.SetActive(false);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    RoomSection.doorNorth.SetActive(false);
                    RoomSection.doorSouth.SetActive(false);
                }
                // If we are on the first column, open the east door
                if (j == 0)
                {
                    RoomSection.doorEast.SetActive(false);
                }
                else if (j == cols - 1)
                {
                    // Otherwise, if we are on the last column row, open the west door
                    RoomSection.doorWest.SetActive(false);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    RoomSection.doorEast.SetActive(false);
                    RoomSection.doorWest.SetActive(false);
                }
                // Save it to the grid array
                GameManager.instance.grid[j, i] = RoomSection;
            }
        }
    }

 
    public GameObject RandomRoomGen()
    {
        return rooms[UnityEngine.Random.Range(0, rooms.Length)];
    }
}
