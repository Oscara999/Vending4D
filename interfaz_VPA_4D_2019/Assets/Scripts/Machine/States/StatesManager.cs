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
    public GamePadCursor inputActions;
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
    bool inGame;
    [SerializeField]
    bool isHereSomeOne;
    [SerializeField]
    bool paymentMade;
    [SerializeField]
    bool challengeAccepted;


    public bool isShowing;
    public int coins;

    public bool IsHereSomeOne { get => isHereSomeOne; set => isHereSomeOne = value; }
    public bool InCinematic { get => inCinematic; set => inCinematic = value; }
    public bool ChallengeAccepted { get => challengeAccepted; set => challengeAccepted = value; }
    public bool InGame { get => inGame; set => inGame = value; }
    public bool PaymentMade { get => paymentMade; set => paymentMade = value; }

    void OnEnable()
    {
        inputActions = new GamePadCursor();
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        //BaseDataManager.Instance.Load();
        StartCoroutine(test());
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
        ControlSystem();
    }

    void ControlSystem()
    {
        //Si se preciona action InsertCoin recargaremos 1 coin
        if (inputActions.Player.InsertCoin.triggered)
        {
            Recharge(1);
        }

        if (inputActions.Player.Menu.triggered)
        {
            ScenesManager.Instance.Pause();
        }

        if (inputActions.Player.Restart.triggered)
        {
            RestartMachine();
        }
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

        skipState.exit = true;
        currentState = skipState;
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
        ui.valuePanel.SetActive(true);
        yield return new WaitUntil(()=> !isShowing);
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
                }
                
                ui.coinsText.text = "Coins:" + coins.ToString();
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