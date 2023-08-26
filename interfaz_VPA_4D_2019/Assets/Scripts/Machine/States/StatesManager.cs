using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatesManager : Singleton<StatesManager>
{
    [Header("Components Settings")]
    public UIController uiController;
    public SegregationController segregationController;
    public LedsController ledsController;
    public InputController inputController;
    public Timer timer;
    public Task skapeTask;
    public GameObject[] hands;

    [Header("Machine States")]
    public KindScene kindScene;
    public ReposeState reposeState;
    public CinematicState cinematicState;
    public SkipState skipState;
    public GameState gameState;
    public State currentState;

    [Header("Stats Settings")]
    [SerializeField]
    bool inCinematic;
    [SerializeField]
    bool inGame;
    [SerializeField]
    bool isHereSomeOne;
    [SerializeField]
    bool paymentMade;
    [SerializeField]
    bool challengeAccepted;
    [SerializeField]
    int coins;

    public bool isShowing;


    public bool IsHereSomeOne { get => isHereSomeOne; set => isHereSomeOne = value; }
    public bool InCinematic { get => inCinematic; set => inCinematic = value; }
    public bool ChallengeAccepted { get => challengeAccepted; set => challengeAccepted = value; }
    public bool InGame { get => inGame; set => inGame = value; }
    public bool PaymentMade { get => paymentMade; set => paymentMade = value; }

    void Start()
    {
        //BaseDataManager.Instance.Load();
        StartCoroutine(test());
        Cursor.visible = false;
    }
    void FixedUpdate()
    {
        HandleStateMachine();
        CoinsValidation();
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2f);
        isHereSomeOne = true;
        Debug.Log("Is there someone");
    }

    public void RestartMachine()
    {
        if (!InGame)
        {
            currentState.ExitState();
            SceneController.Instance.QTEManager.eventData = null;
            SceneController.Instance.QTEManager.doFinally();
            DialogueSystem.Instance.EndDialogue(); 
        }
        else
        {
            ManagerGame.Instance.QTEManager.eventData = null;
            ManagerGame.Instance.QTEManager.doFinally();
        }

        skipState.exit = true;
        currentState = skipState;
    }
    public void SkipMachine()
    {
        if (!InGame)
        {
            currentState.ExitState();
            SceneController.Instance.QTEManager.eventData = null;
            SceneController.Instance.QTEManager.doFinally();
            DialogueSystem.Instance.EndDialogue();
        }
        else
        {
            ManagerGame.Instance.QTEManager.eventData = null;
            ManagerGame.Instance.QTEManager.doFinally();
        }

        skipState.exit = true;
        currentState = skipState;
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
            Debug.Log("Don't have coins");
            return paymentMade = false;
        }
    }

    public void SwitchToNextState(State state)
    {
        currentState = state;
        Debug.Log(currentState.name);
    }

    public IEnumerator ShowValuePanel()
    {
        uiController.valuePanel.SetActive(true);
        yield return new WaitUntil(()=> !isShowing);
        uiController.valuePanel.SetActive(false);
    }

    public void Segregation()
    {

    }

    public void CoinsValidation()
    {
        if (currentState != reposeState)
        {
            if (coins > 0)
            {
                if (!uiController.coinsText.gameObject.activeInHierarchy)
                {
                    uiController.coinsText.gameObject.SetActive(true);
                }

                uiController.coinsText.text = "Coins:" + coins.ToString();
            }
            else
            {
                uiController.coinsText.gameObject.SetActive(false);
            }
        }
        else
        {
            uiController.coinsText.gameObject.SetActive(false);
        }
        
    }

    public void Check(bool validation)
    {
        isHereSomeOne = validation;
    }
}