using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    string name;
    float score;

    

    public void Save(string highScore,  string Name)
    {
        PlayerPrefs.SetString( highScore, Name);
    }

    public void Load()
    {
       // theText.text = PlayerPrefs.GetString("TextData");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
