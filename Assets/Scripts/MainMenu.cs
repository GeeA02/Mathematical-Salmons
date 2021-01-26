using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;

    private void OnMouseUp()
    {
        if(isStart)
        {
            SceneManager.LoadScene("tmp");
        }
        if(isQuit)
        {
            Application.Quit();
        }
    }
}
