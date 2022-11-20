﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueSystem : Singleton<DialogueSystem>
{
    Queue<string> sentences;
    List<Sound> provicionalSounds = new List<Sound>();
    Dialogue newDialogue;
    int index;

    public bool inPlaying;
    public GameObject panelDialogue;

    [SerializeField] TMP_Text textBox;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void ChangeStateBoxDialogue(bool state)
    {
          panelDialogue.SetActive(state);
    }

    public void StartNewDialogue(Dialogue dialogue)
    {
        newDialogue = dialogue;
        ChangeStateBoxDialogue(true);
        StartDialogue();
        inPlaying = true;
    }

    public void StartDialogue()
    {
        sentences.Clear();

        if (newDialogue.sounds.Count > 0)
        {
            for (int i = 0; i < newDialogue.sequences.Length; i++)
            {
                provicionalSounds.Add(newDialogue.sounds[i]);
            }
        }

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

        if (provicionalSounds.Count > 0)
        {
            SoundManager.Instance.PlayNewSound(provicionalSounds[index].name);
        }

        index++;

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        textBox.text = "";
        yield return new WaitForSeconds(0.01f);
        textBox.text += sentence;
    }

    public void EndDialogue()
    {
        index = 0;
        provicionalSounds.Clear();
        inPlaying = false;
        sentences.Clear();
        ChangeStateBoxDialogue(false);
        newDialogue = null;
    }
}