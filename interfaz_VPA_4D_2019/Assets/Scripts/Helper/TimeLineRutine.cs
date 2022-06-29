using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineRutine : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors = new List<PlayableDirector>();

    public bool StatePlayable(int index)
    {
        if (playableDirectors[index].state != PlayState.Playing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Play(int index)
    {
        playableDirectors[index].Play();
    }

    public void Pause(int index)
    {
        playableDirectors[index].Pause();
    }

    public void Resume(int index)
    {
        playableDirectors[index].Resume();
    }
}

