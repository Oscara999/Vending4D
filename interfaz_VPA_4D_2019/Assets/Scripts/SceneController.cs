using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    public UIController uIController;
    public GameObject timeLines;
    public Dialogue[] dialogues;

    void Start()
    {
        StatesManager.Instance.SetChangeTimeLine(timeLines);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDialogue()
    {
        if (DialogueSystem.Instance == null)
            return;

        DialogueSystem.Instance.DisplayNextDialogue();
    }
    public void StartDialogue(int index)
    {
        if (DialogueSystem.Instance == null)
            return;
        
        DialogueSystem.Instance.StartNewDialogue(dialogues[index]);
    }


}
