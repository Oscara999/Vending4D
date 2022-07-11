using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    public TimeLineRutine timeLineRutine;
    public UIController uIController;


    void Start()
    {
        StatesManager.Instance.TimeLineRutine = timeLineRutine;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
