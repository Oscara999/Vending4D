using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineRutine : MonoBehaviour
{
    [SerializeField]
    List<PlayableDirector> playableDirectors = new List<PlayableDirector>();


    public void SetCinematics(GameObject Container)
    {
        playableDirectors.Clear();

        foreach (PlayableDirector playable in Container.GetComponentsInChildren<PlayableDirector>())
        {
            playableDirectors.Add(playable);
        }
    }

    public bool StatePlayable(int index)
    {
        if (playableDirectors[index].state == PlayState.Playing 
            && playableDirectors[index].state != PlayState.Paused )
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
        Debug.Log(playableDirectors[index].name);
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

