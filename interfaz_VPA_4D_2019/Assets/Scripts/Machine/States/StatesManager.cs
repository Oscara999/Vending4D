using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatesManager : Singleton<StatesManager>
{
    [Header("Components Settings")]
    public LedsController ledsController;
    public TimeLineRutine timeLineRutine;
    public GameObject[] hands;
    public Task skapeTask;
    public State currentState;

    [Header(" UI Settings")]
    public UIController uIController;
    public Canvas canvas;
    public GameObject questPanel;
    public GameObject pausePanel;
    public GameObject rulesPanel;


    [SerializeField]
    GameObject valuePanel;
    [SerializeField]
    TMP_Text coinsText;

    [Header("Stats Settings")]
    [SerializeField]
    bool inCinematic;
    [SerializeField]
    bool isHereSomeOne;
    [SerializeField]
    bool paymentMade;
    [SerializeField]
    bool challengeAccepted;
    [SerializeField]
    bool inGame;
    public int coins;

    public bool IsHereSomeOne { get => isHereSomeOne; set => isHereSomeOne = value; }
    public bool InCinematic { get => inCinematic; }
    public bool ChallengeAccepted { get => challengeAccepted; set => challengeAccepted = value; }
    public bool InGame { get => inGame; set => inGame = value; }
    public bool ValuePanel { get => valuePanel.activeInHierarchy; }
    public bool PaymentMade { get => paymentMade; set => paymentMade = value; }

    void Start()
    {
        //BaseDataManager.Instance.Load();
       // Cursor.visible = false;
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2f);
        isHereSomeOne = true;
        Debug.Log("Is there someone");
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
                SwitchToNextState(nextState);
            }
        }
    }
    
    public void Recharge(int numCoins)
    {
        coins += numCoins;
        Debug.Log("recharged: "+ coins);
    }

    public void SetChallengeStatus(bool state)
    {
        challengeAccepted = state;
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
            Debug.Log("Don't have any coin");
            return paymentMade = false;
        }
    }

    public void SwitchToNextState(State state)
    {
        currentState = state;
        
        if (currentState.name == "GameState")
        {
            ScenesManager.Instance.isLoad = true;
            ScenesManager.Instance.LoadLevel("Test3");
        }

        Debug.Log(currentState.name);
    }

    public void SetChangeTimeLine(GameObject container)
    {
        if (timeLineRutine != null)
        {
            timeLineRutine.SetCinematics(container);
        }
    }

    public IEnumerator ShowValuePanel(float waitTime)
    {
        valuePanel.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        valuePanel.SetActive(false);
    }

    public void Segregation()
    {

    }
    public void CoinsValidation()
    {
        if (!inGame)
        {
            if (!coinsText.gameObject.activeInHierarchy)
            {
                coinsText.gameObject.SetActive(true);
            }
        }
        else
        {
            coinsText.gameObject.SetActive(false);
        }
        
        coinsText.text = "Coins:" + coins.ToString();
    }

    public void Check(bool validation)
    {
        isHereSomeOne = validation;
    }
}