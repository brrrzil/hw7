using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageTimer : MonoBehaviour
{

    public float maxTime;

    private Image img;
    private float currentTime;

    void Start()
    {
        img = GetComponent<Image>();
        currentTime = 0;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            currentTime = 0;
        }

        img.fillAmount = currentTime / maxTime;
    }
}
