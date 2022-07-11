using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager : Singleton<StatesManager>
{
    public State currentState;
    public LedsController ledsController;
    
    TimeLineRutine timeLineRutine;
    [SerializeField]
    bool isThereSomeone;
    int coins;

    public int Coins { get => coins; set => coins = value; }
    public bool IsThereSomeone { get => isThereSomeone; set => isThereSomeone = value; }
    public TimeLineRutine TimeLineRutine { get => timeLineRutine; set => timeLineRutine = value; }

    void Start()
    {
        //BaseDataManager.Instance.Load();
    }

    void Update()
    {
        CoinsValidation();
    }

    void FixedUpdate()
    {
        HandleStateMachine();
    }

    void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick();

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    public void StartGame()
    {

    }

    public void LightsUpdate()
    {

    }

    void SwitchToNextState(State state)
    {
        currentState = state;
    }

    public void ChangeTimeLine()
    {
        if (timeLineRutine != null)
        {

        }
    }

    public void Segregation()
    {

    }

    public void Subtitles()
    {

    }

    public void CoinsValidation()
    {
        if (coins > 0)
        {

        }
    }

    public void Check(bool validation)
    {
        isThereSomeone = validation;
    }
}