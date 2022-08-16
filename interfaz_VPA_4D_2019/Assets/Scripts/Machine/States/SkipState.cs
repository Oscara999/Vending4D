using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SkipState : State
{
    [Header("Next States")]
    public ReposeState reposeState;
    public GameState gameState;
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
            nextState = gameState;
            ScenesManager.Instance.LoadLevel("Test3");
        }
        else
        {
            StatesManager.Instance.IsHereSomeOne = false;
            StatesManager.Instance.skapeTask.RestartSize(false);
            StatesManager.Instance.ledsController.ramdom = true;
            nextState = reposeState;
        }

        exit = false;
    }

    IEnumerator EnabledRules()
    {
        StatesManager.Instance.rulesPanel.SetActive(true);
        videoPlayer.Play();
        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        yield return new WaitForSeconds(1.5f);
        StatesManager.Instance.rulesPanel.SetActive(false);
        StatesManager.Instance.InGame = true;
        exit = true;
    }
}
