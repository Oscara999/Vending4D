using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueSystem : Singleton<DialogueSystem>
{
    [HideInInspector] public Queue<string> sentences;
    public GameObject panelDialogue;
    [SerializeField] TMP_Text textBox;
    [SerializeField] Dialogue newDialogue;

    bool inPlaying;
    private float deltaTime;
    private float msec;
    private float fps;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void ChangeStateBoxDialogue()
    {
        if (!panelDialogue.activeInHierarchy)
        {
            panelDialogue.SetActive(true);
        }
    }


    public void StartNewDialogue(Dialogue dialogue)
    {
        newDialogue = dialogue;
        ChangeStateBoxDialogue();
        StartDialogue();
        inPlaying = true;
    }

    public void StartDialogue()
    {
        sentences.Clear();

        foreach (string sentence in newDialogue.sequences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    void Update()
    {
        if (inPlaying)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            CalculateFps();
        }
    }

    void CalculateFps()
    {
        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
    }

    IEnumerator TypeSentence(string sentence)
    {
        textBox.text = "";
       
        yield return new WaitForSeconds(0.01f);

        if (fps < 55)
        {
            textBox.text += sentence;
            Debug.Log("Que computador tan lento");
        }
        else
        {
            foreach (char letter in sentence.ToCharArray())
            {
                textBox.text += letter;
                yield return null;
            }
        }

        yield return new WaitForSeconds(2.5f);
    }

    public void EndDialogue()
    {
        inPlaying = false;
        msec = fps = 0;
        sentences.Clear();
        panelDialogue.SetActive(false);
        newDialogue = null;
    }
}