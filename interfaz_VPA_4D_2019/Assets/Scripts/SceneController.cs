using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    public GameObject timeLines;
    public GameObject QTE;
    public Dialogue[] dialogues;

    void Awake()
    {
        base.Awake();
        StatesManager.Instance.SetChangeTimeLine(timeLines);
    }

    public void ValidatePayment()
    {
        StatesManager.Instance.SubtractCoin();
    }

    public void ValidatePlayGame()
    {
        StatesManager.Instance.ValidateChallengeStatus();
    }

    public void NextDialogue()
    {
        if (DialogueSystem.Instance == null)
            return;

        DialogueSystem.Instance.DisplayNextDialogue();
    }

    public void StartQuestPanel(bool value)
    {
        StatesManager.Instance.questPanel.SetActive(value);
    }

        public void StartShowPanel(float timeStart)
    {
      StartCoroutine(StatesManager.Instance.ShowValuePanel(timeStart));
    }

    public void StartDialogue(int index)
    {
        if (DialogueSystem.Instance == null)
            return;
        
        DialogueSystem.Instance.StartNewDialogue(dialogues[index]);
    }
}
