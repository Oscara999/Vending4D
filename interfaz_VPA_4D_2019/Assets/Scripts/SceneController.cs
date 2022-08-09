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

    public IEnumerator StartQuestTimer()
    {
        StatesManager.Instance.questPanel.SetActive(true);
        //mostrar reloj
        yield return new WaitForSeconds(10f);
        //seleccioar respuesta correcta 
        SoundManager.Instance.PlayNewSound("SelectedFinish");
        // validar si acepta o no el reto para definir acciones
        StatesManager.Instance.ChallengeAccepted = true;
        yield return new WaitForSeconds(2f);
        StatesManager.Instance.questPanel.SetActive(false);
    }

    public void ValidatePayment()
    {
        StatesManager.Instance.SubtractCoin();
    }

    public void NextDialogue()
    {
        if (DialogueSystem.Instance == null)
            return;

        DialogueSystem.Instance.DisplayNextDialogue();
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
