using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void SetAudioLevel(float volume)
    {
        AudioListener.volume = Mathf.Log10(volume * 20);
    }
}
