using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;
    public AudioSource Click; 

    private void OnMouseUp()
    {
        Click.Play();
        if(isStart)
        {
            SceneManager.LoadScene("map1");
        }
        if(isQuit)
        {
            Application.Quit();
        }
    }
}
