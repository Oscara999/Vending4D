using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    public GameObject timeLines;
    public QTEManager QTEManager;
    public QTEEvent[] QTE;
    public Dialogue[] dialogues;
    public bool setBoolStartGame;
    public TimeLineRutine timeLineRutine;

    void Awake()
    {
        base.Awake();
        SetChangeTimeLine(timeLines);
        StatesManager.Instance.kindScene = KindScene.Menu;
    }

    void SetChangeTimeLine(GameObject container)
    {
        if (timeLineRutine != null)
        {
            timeLineRutine.SetCinematics(container);
        }
    }


    public void StartResultsEvent()
    {
        Debug.Log("next Part 2");
        StartCoroutine(StatesManager.Instance.cinematicState.SendResultsAcceptPlay());
    }

    public void ValidatePayment()
    {
        StatesManager.Instance.SubtractCoin();
    }

    public void ValidatePlayGame()
    {
        MouseEvent mouseEventTrue = GameObject.FindGameObjectWithTag("MouseEventTrue").GetComponent<MouseEvent>();

        if (!setBoolStartGame)
        {
            Debug.Log("no respondio nada");
            mouseEventTrue.Change(mouseEventTrue.selectedPanelImage);
            StatesManager.Instance.SetChallengeStatus(true);
            setBoolStartGame = true;
        }
        else
        {
            mouseEventTrue.Change(mouseEventTrue.normalPanelImage);
            mouseEventTrue.isSelected = mouseEventTrue.anotherButton.isSelected = false;
        }
    }

    public void NextDialogue()
    {
        if (DialogueSystem.Instance == null)
            return;

        DialogueSystem.Instance.DisplayNextDialogue();
    }

    public void StartQuestPanel(bool value)
    {
        StatesManager.Instance.uiController.questPanel.SetActive(value);
    }

    public void StartShowPanel(float timeStart)
    {
        StatesManager.Instance.isShowing = true; 
        StartCoroutine(StatesManager.Instance.ShowValuePanel());
    }

    public void StartDialogue(int index)
    {
        if (DialogueSystem.Instance == null)
            return;
        
        DialogueSystem.Instance.StartNewDialogue(dialogues[index]);
    }
}
