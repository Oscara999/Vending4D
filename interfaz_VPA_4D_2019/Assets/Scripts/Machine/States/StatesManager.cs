﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatesManager : Singleton<StatesManager>
{
    [Header("Components Settings")]
    public LedsController ledsController;
    public TimeLineRutine timeLineRutine;
    [SerializeField]
    State currentState;

    [Header(" UI Settings")]
    public RectTransform canvasRectTransform;
    public Canvas canvas;
    public RectTransform cursorTransform;
    public GameObject questPanel;

    [SerializeField] 
    GameObject valuePanel;
    [SerializeField]
    TMP_Text coinsText;

    [Header("Stats Settings")]
    [SerializeField]
    bool inCinematic;
    [SerializeField]
    bool isThereSomeone;
    [SerializeField]
    bool paymentMade;
    [SerializeField]
    bool stateValuePanel;
    [SerializeField]
    bool challengeAccepted;
    [SerializeField]
    bool inGame;
    [SerializeField]
    int coins;


    public int Coins { get => coins; set => coins = value; }
    public bool IsThereSomeone { get => isThereSomeone; set => isThereSomeone = value; }
    public bool PaymentMade { get => paymentMade; set => paymentMade = value; }
    public bool StateValuePanel { get => stateValuePanel; set => stateValuePanel = value; }
    public bool InCinematic { get => inCinematic;}
    public bool ChallengeAccepted { get => challengeAccepted; set => challengeAccepted = value; }
    public bool InGame { get => inGame; set => inGame = value; }

    void Start()
    {
        //BaseDataManager.Instance.Load();

    }

    void Update()
    {
        CoinsValidation();
    }

    public void StateInCinematic(bool value)
    {
        inCinematic = value;
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

            if (nextState != currentState)
            {
                Debug.Log("check");
                SwitchToNextState(nextState);
            }
        }
    }

    public bool SubtractCoin()
    {
        if (coins > 0)
        {
            coins--;
            Debug.Log("Coin Subtracted");
            return paymentMade = true;
        }
        else
        {
            return paymentMade = false;
        }
    }

    void SwitchToNextState(State state)
    {
        currentState = state;
        Debug.Log(currentState.name);
    }

    public void SetChangeTimeLine(GameObject container)
    {
        if (timeLineRutine != null)
        {
            timeLineRutine.SetCinematics(container);
        }
    }

    public IEnumerator ShowValuePanel(float startTime, float waitTime, float endTime)
    {
        yield return new WaitForSeconds(startTime);
        
        while (stateValuePanel)
        {
            valuePanel.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            valuePanel.SetActive(false);
            yield return new WaitForSeconds(endTime);
        }        
    }

    public void Segregation()
    {

    }
    public void CoinsValidation()
    {
        if (coins == 0)
        {
            if (!coinsText.gameObject.activeInHierarchy)
            {
                coinsText.gameObject.SetActive(true);
            }

            coinsText.text = "Coins " + coins.ToString();
        }
        else
        {
            coinsText.gameObject.SetActive(false);
        }
    }

    public void Check(bool validation)
    {
        isThereSomeone = validation;
    }
}