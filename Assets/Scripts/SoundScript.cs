using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    private AudioSource aud;
    private Image img;

    public Sprite SpriteOn, SpriteOff;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.Play();
        img = GetComponent<Image>();
    }

    public void PlayPause()
    {
        if (aud.isPlaying)
        {
            aud.Pause();
            img.sprite = SpriteOff;
        }
        else
        {
            aud.Play();
            img.sprite = SpriteOn;
        }
    }

    void Update()
    {
        
    }
}
