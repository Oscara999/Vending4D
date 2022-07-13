using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager : Singleton<StatesManager>
{
    public LedsController ledsController;
    
    [SerializeField] 
    GameObject valuePanel;
    [SerializeField]
    State currentState;
    [SerializeField]
    TimeLineRutine timeLineRutine;

    [Header("Stats Settings")]
    [SerializeField]
    bool isThereSomeone;
    [SerializeField]
    bool paymentMade;
    [SerializeField]
    int repeatShowPanel;
    [SerializeField]
    int coins;

    
    public int Coins { get => coins; set => coins = value; }
    public bool IsThereSomeone { get => isThereSomeone; set => isThereSomeone = value; }
    public TimeLineRutine TimeLineRutine { get => timeLineRutine; set => timeLineRutine = value; }
    public bool PaymentMade { get => paymentMade; set => paymentMade = value; }
    public int RepeatShowPanel { get => repeatShowPanel; set => repeatShowPanel = value; }

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
        if (ScenesManager.Instance.systemPrefabs[1].activeInHierarchy)
            return;
        
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

    public IEnumerator ShowValuePanel(float startTime, float waitTime, float endTime)
    {
        yield return new WaitForSeconds(startTime);
        
        while (repeatShowPanel > 0)
        {
            valuePanel.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            valuePanel.SetActive(false);
            yield return new WaitForSeconds(endTime);
            repeatShowPanel--;
        }        
        Debug.Log(1230);
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