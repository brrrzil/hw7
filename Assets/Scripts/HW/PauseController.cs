using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool paused;

    public AudioSource Sound;

    public void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1;
            Sound.Play();
        }

        else
        {
            Time.timeScale = 0;
            Sound.Pause();
        }

        paused = !paused;
    }
}
