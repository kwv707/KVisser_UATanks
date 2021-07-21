using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public AudioSource Audio;

    public void TestTheButton()
    {
        Audio.Play();
        Debug.Log("The button was pressed.");
    }
}
