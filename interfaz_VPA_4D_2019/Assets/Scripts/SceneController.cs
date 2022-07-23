using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SceneController : Singleton<SceneController>
{
    public UIController uIController;
    public GameObject timeLines;
    public Dialogue[] dialogues;
    public GameObject questPanel;

    void Start()
    {
        StatesManager.Instance.SetChangeTimeLine(timeLines);
    }

    public IEnumerator StartTimer()
    {
        questPanel.SetActive(true);
        //mostrar reloj
        yield return new WaitForSeconds(10f);
        //seleccioar respuesta correcta 
        SoundManager.Instance.PlayNewSound("SelectedFinish");

        yield return new WaitForSeconds(2f);
        questPanel.SetActive(false);
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
