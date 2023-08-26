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
    bool readyToExit;
    public VideoPlayer videoPlayer;

    State nextState;

    public override State Tick()
    {
        if (readyToExit)
        {
            readyToExit = false;
            return nextState;
        }

        if (exit)
        {
            ExitState();
            return this;
        }
        else
        {
            if (isRulesTime)
            {
                StartCoroutine(EnabledRules());
                isRulesTime = false;
            }
            Debug.Log("Waiting to exit");
            return this;
        }
    }

    public override void ExitState()
    {
        if (!StatesManager.Instance.InGame)
        {
            if (StatesManager.Instance.InCinematic)
            {
                Debug.Log("RestartFromMottisScene");
                StatesManager.Instance.IsHereSomeOne = false;
                StatesManager.Instance.ledsController.ramdom = true;
                nextState = StatesManager.Instance.reposeState;
                StopAllCoroutines();
                StartCoroutine(ExitLoad("Introduccion_Mottis1", "Introduccion_Mottis1"));
            }
            else
            {
                Debug.Log("GonnaPlayGame");
                StatesManager.Instance.InGame = true;
                StatesManager.Instance.ledsController.ramdom = false;
                StatesManager.Instance.IsHereSomeOne = false;
                nextState = StatesManager.Instance.gameState;
                StopAllCoroutines();
                StartCoroutine(ExitLoad("Introduccion_Mottis1", "Test3"));
            }
        }
        else
        {
            StatesManager.Instance.ledsController.ramdom = true;
            nextState = StatesManager.Instance.reposeState;
            StopAllCoroutines(); 
            StatesManager.Instance.InGame = false;
            Debug.Log("RestartFromGame");
            StartCoroutine(ExitLoad("Test3", "Introduccion_Mottis1"));
        }

        StatesManager.Instance.isShowing = false;
        StatesManager.Instance.uiController.valuePanel.SetActive(false);
        
        SoundManager.Instance.PauseAllSounds(true);
        exit = false;
    }

    IEnumerator ExitLoad(string lastSceneName , string newSceneName)
    {
        StatesManager.Instance.skapeTask.ChangeSize(true);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);

        ScenesManager.Instance.UnLoadLevel(lastSceneName);
        yield return new WaitForSeconds(0.5f);
        ScenesManager.Instance.CurrentLevelName = string.Empty;
        ScenesManager.Instance.LoadLevel(newSceneName);
        yield return new WaitForSeconds(0.5f);
        readyToExit = true;
    }

    IEnumerator EnabledRules()
    {
        StatesManager.Instance.uiController.rulesPanel.SetActive(true);
        videoPlayer.Play();
        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        yield return new WaitForSeconds(2f);
        StatesManager.Instance.uiController.rulesPanel.SetActive(false);
        StatesManager.Instance.IsHereSomeOne = false;
        StatesManager.Instance.gameState.startGame = true;
        yield return new WaitForSeconds(2f);
        exit = true;
    }
}
