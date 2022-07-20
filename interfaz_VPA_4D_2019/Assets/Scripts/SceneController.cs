using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    public UIController uIController;
    public GameObject timeLines;

    void Start()
    {
        StatesManager.Instance.SetChangeTimeLine(timeLines);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
