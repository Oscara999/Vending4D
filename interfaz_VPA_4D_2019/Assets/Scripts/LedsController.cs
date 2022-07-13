using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedsController : MonoBehaviour
{
    public bool isActivate;
    public bool ramdom;
    public float nextTimeToSwitch;
    public Image referenceImage;
    public Color newColor;


    float timer;

    void FixedUpdate()
    {
        if (!isActivate)
            return;

        referenceImage.color = newColor;

        if (ramdom)
        {
            RandomColor();
        }
    }

    public void RandomColor()
    {
        timer += Time.deltaTime;
        if (timer >= nextTimeToSwitch) 
        {
            newColor = new Color(Random.value, Random.value, Random.value, 100);
            timer = 0;
        }
    }

    public void SetColor(Color color)
    {
        newColor = color;
    }

}
