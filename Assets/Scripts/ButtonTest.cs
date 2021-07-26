using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonTest : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip ButtonSound;
    public AudioClip BackgroundMusic;
    private GameObject MapGenerator;
    public Slider slider;
    public Toggle MuteToggle;
    public AudioListener audioListener;
    public Canvas Options;
    public Canvas MainMenu;
    
    public void Start()
    {
        Audio.PlayOneShot(BackgroundMusic);
        Options.enabled = false;

    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            MainMenu.enabled = !MainMenu.enabled;
            Options.enabled = !Options.enabled;
            

        }
    }

    public void OptionsOpen()
    {
        
        MainMenu.enabled = false;
        Options.enabled = true;
    }
    public void CloseOptions()
    {
        MainMenu.enabled = true;
        Options.enabled = false;
    }

    public void StartGame()
    {
        Audio.PlayOneShot(ButtonSound);
        SceneManager.LoadScene("TestMap");
        GameManager.instance.isSinglePlayerMode = true;
        
        Debug.Log("The button was pressed.");
    }

    public void MultiplayerMode()
    {
        
        Audio.PlayOneShot(ButtonSound);
        SceneManager.LoadScene("TestMap"); GameManager.instance.GetComponent<GameManager>().isSinglePlayerMode = false;
        Debug.Log("The button was pressed.");
        
    }

    public void RandomMap()
    {
        GameManager.instance.GetComponent<MapGenerator>().RandoMapMode = !GameManager.instance.GetComponent<MapGenerator>().RandoMapMode;
    }

    public void MapOfTheDay()
    {
        GameManager.instance.GetComponent<MapGenerator>().MapOfTheDay = !GameManager.instance.GetComponent<MapGenerator>().MapOfTheDay;
    }
    public void VolumeControl()
    {

        AudioListener.volume = slider.value;
    }

    public void VolumeMute()
    {        
            AudioListener.pause = !AudioListener.pause;
        Debug.Log("Volume Muted");
    }

    public void ExitGame()
    {
        Audio.PlayOneShot(ButtonSound);
        print("Quit Game");
        Application.Quit();
    }





}
