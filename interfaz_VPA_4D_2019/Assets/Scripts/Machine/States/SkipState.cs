using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipState : State
{
    [Header("Next States")]
    public bool isRulesTime;
    public bool exit;
    public VideoPlayer videoPlayer;

    State nextState;

    public override State Tick()
    {
        if (exit)
        {
            ExitState();
            return nextState;
        }
        else
        {
            if (isRulesTime)
            {
                StartCoroutine(EnabledRules());
                isRulesTime = false;
            }
            Debug.Log("waiting to exit");
            return this;
        }
    }

    protected override void ExitState()
    {
        if (StatesManager.Instance.InGame)
        {
            StatesManager.Instance.ledsController.ramdom = false;
            StatesManager.Instance.IsHereSomeOne = false;
            nextState = StatesManager.Instance.gameState;
            StartCoroutine(ExitLoad("Introduccion_Mottis", "Test3"));
        }
        else
        {
            StatesManager.Instance.IsHereSomeOne = false;
            StatesManager.Instance.ledsController.ramdom = true;
            nextState = StatesManager.Instance.reposeState;
            StartCoroutine(ExitLoad("Introduccion_Mottis", "Introduccion_Mottis"));
        }
        exit = false;
    }

    IEnumerator ExitLoad(string lastSceneName , string newSceneName)
    {
        ScenesManager.Instance.UnLoadLevel(lastSceneName);
        yield return new WaitForSeconds(1f);
        ScenesManager.Instance.CurrentLevelName = string.Empty;
        ScenesManager.Instance.LoadLevel(newSceneName);
    }

    IEnumerator EnabledRules()
    {
        StatesManager.Instance.ui.rulesPanel.SetActive(true);
        videoPlayer.Play();
        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        yield return new WaitForSeconds(1.5f);
        StatesManager.Instance.ui.rulesPanel.SetActive(false);
        StatesManager.Instance.InGame = true;
        StatesManager.Instance.IsHereSomeOne = false;
        exit = true;
    }
}
