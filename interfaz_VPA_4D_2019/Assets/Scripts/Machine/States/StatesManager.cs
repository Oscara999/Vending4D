using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatesManager : Singleton<StatesManager>
{
    [Header("Components Settings")]
    public LedsController ledsController;
    public GameObject[] hands;
    public Task skapeTask;
    public UI ui;

    [Header("States")]
    public ReposeState reposeState;
    public CinematicState cinematicState;
    public SkipState skipState;
    public GameState gameState;
    public State currentState;

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
    public bool PaymentMade { get => paymentMade; set => paymentMade = value; }

    void Start()
    {
        //BaseDataManager.Instance.Load();
        //StartCoroutine(test());
       // Cursor.visible = false;
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(1f);
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
            ScenesManager.Instance.LoadLevel("Test3");
        }

        Debug.Log(currentState.name);
    }

    public IEnumerator ShowValuePanel(float waitTime)
    {
        ui.valuePanel.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ui.valuePanel.SetActive(false);
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
                if (!ui.coinsText.gameObject.activeInHierarchy)
                {
                    ui.coinsText.gameObject.SetActive(true);
                    ui.coinsText.text = "Coins:" + coins.ToString();
                }
            }
            else
            {
                ui.coinsText.gameObject.SetActive(false);
            }
        }
        else
        {
            ui.coinsText.gameObject.SetActive(false);
        }
        
    }

    public void Check(bool validation)
    {
        isHereSomeOne = validation;
    }
}

[System.Serializable]
public class UI
{
    [Header(" UI Settings")]
    public UIController uIController;
    public Canvas canvas;
    public GameObject questPanel;
    public GameObject pausePanel;
    public GameObject rulesPanel;
    public GameObject valuePanel;
    public GameObject[] DesingCursor;
    public TMP_Text coinsText;
    public GameObject[] eventUI;
    public Text eventTimerText;
    public Image eventTimerImage;
}