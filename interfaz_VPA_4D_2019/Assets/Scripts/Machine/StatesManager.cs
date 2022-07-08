using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager : Singleton<StatesManager>
{
    public TimeLineRutine timeLineRutine;
    public State currentState;
    public bool isThereSomeone;
    public Movimiento_UI_Control_Juego movimiento;
    public UIController uIController;
    LedsController ledsController;

    void Start()
    {
        //BaseDataManager.Instance.Load();
    }

    void Update()
    {
        CoinsValidation();
        movimiento.Selected();
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

    }

    public void Segregation()
    {

    }

    public void Subtitles()
    {

    }

    public void CoinsValidation()
    {

    }

}