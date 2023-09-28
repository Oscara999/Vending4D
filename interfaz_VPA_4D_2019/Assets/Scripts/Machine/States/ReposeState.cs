using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class ReposeState : State
{
    public GameObject demoPanel;
    public VideoPlayer videoPlayer;
    public VideoClip[] videos;
    public int currentVideoIndex;

    void StartMachine()
    {
        StatesManager.Instance.ledsController.ramdom = true;
        currentVideoIndex = 0;
        videoPlayer.clip = videos[currentVideoIndex];
        demoPanel.SetActive(true);
        videoPlayer.Play();
    }

    public override State Tick()
    {
        if (!StatesManager.Instance.IsHereSomeOne)
        {
            Check();
            return this;
        }
        else
        {
            ExitState();
            return StatesManager.Instance.cinematicState;
        }
    }

    void Check()
    {
        if (!demoPanel.activeInHierarchy)
        {
            StartMachine();
        }

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
        }
    }

    public override void ExitState()
    {
        StatesManager.Instance.cinematicState.ChangeState(1);
        demoPanel.SetActive(false); 
        videoPlayer.Stop();
        SoundManager.Instance.PlayNewSound("BackGroundMainManu");
        StopAllCoroutines();
        StatesManager.Instance.InCinematic = true;
    }
}
