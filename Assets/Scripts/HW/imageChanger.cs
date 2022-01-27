using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageChanger : MonoBehaviour
{
    public Sprite newSprite;
    public AudioClip Clip;

    private Image img;
    private AudioSource aud;


    void Start()
    {
        img = GetComponent<Image>();
        aud = GetComponent<AudioSource>();
        aud.Play();
    }

    public void ChangeSprite()
    {
        img.sprite = newSprite;
        img.SetNativeSize();
    }

    public void ChangeColor()
    {
        //img.color = Color.magenta;
        img.color = new Color(0.1f, 0.2f, 0.3f);
    }

    public void ChangeSoundPlay()
    {
        if (aud.isPlaying) aud.Pause();
        else aud.Play();
    }

    public void ChangeSound()
    {
        aud.clip = Clip;
        aud.Play();
    }

    void Update()
    {
        
    }
}
