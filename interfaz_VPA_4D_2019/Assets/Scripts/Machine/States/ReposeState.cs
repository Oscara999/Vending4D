using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class ReposeState : State
{
    public IntroductionState introductionState;
    public GameObject demoPanel;
    public VideoPlayer videoPlayer;
    public VideoClip[] videos;
    public int currentVideoIndex;


    void Start()
    {
        demoPanel.SetActive(true);
        currentVideoIndex = 0;
        videoPlayer.clip = videos[currentVideoIndex];
        videoPlayer.Play();
    }

    public override State Tick()
    {
        if (StatesManager.Instance.IsThereSomeone == false)
        {
            Check();
            return this;
        }
        else
        {
            ExitState();
            return introductionState;
        }
    }

    void Check()
    {
        if (!demoPanel.activeInHierarchy)
        {
            Start();
        }
        
        //validar el sensor de que alguien entra a la maquina
        //mostrar videos 
        ChangeVideo();
        //Camiar color leds
        // activar letrero de precio
    }

    void ExitState()
    {
        demoPanel.SetActive(false);
        videoPlayer.Stop();
        SoundManager.Instance.PlayNewSound("BackGroundMainManu");
        StartCoroutine(SceneController.Instance.uIController.StartTimer());
    }

    void ChangeVideo()
    {
        if (videoPlayer.clip != null && !videoPlayer.isPlaying)
        {
            if (currentVideoIndex == videos.Length - 1)
            {
                currentVideoIndex = 0;
                StatesManager.Instance.Check(true);
            }
            else
            {
                currentVideoIndex++;
            }

            videoPlayer.clip = videos[currentVideoIndex];
            videoPlayer.Play();
            //Segregar olor
        }
    }
}
