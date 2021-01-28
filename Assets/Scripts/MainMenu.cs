using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;
    public bool isOptions;
    public bool isAbout;
    public bool isBack;
    public AudioSource Click; 

    private void OnMouseUp()
    {
        Click.Play();
        if(isStart)
        {
            SceneManager.LoadScene("map1");
        }
        if (isOptions)
        {
            SceneManager.LoadScene("options");
        }
        if (isAbout)
        {
            SceneManager.LoadScene("about");
        }
        if (isQuit)
        {
            Application.Quit();
        }
        if (isBack)
        {
            SceneManager.LoadScene("menu");
        }
    }
}
