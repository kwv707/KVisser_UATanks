using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;           // needs to be added in order to use Text class

public class SaveManager : MonoBehaviour
{
    string name;
    float score;

    public Text theText;

    public void Save()
    {
        PlayerPrefs.SetString("TextData"/*data to be stored for later*/, theText.text/*data variable to be used*/);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        theText.text = PlayerPrefs.GetString("TextData");
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
