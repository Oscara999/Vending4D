using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public int startMinutes;
    public Text ui;
    public bool timerActive;

    
    void Update()
    {
        if (timerActive)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                timerActive = false;
            }

            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            ui.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        }
    }

    public void StartTimer()
    {
        currentTime = startMinutes * 60;
        timerActive = true;
    }
}
