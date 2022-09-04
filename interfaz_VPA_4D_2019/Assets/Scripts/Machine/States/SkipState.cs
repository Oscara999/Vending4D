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
            nextState = StatesManager.Instance.gameState;
            ScenesManager.Instance.LoadLevel("Test3");
        }
        else
        {
            StatesManager.Instance.IsHereSomeOne = false;
            StatesManager.Instance.ledsController.ramdom = true;
            nextState = StatesManager.Instance.reposeState;
            //SceneManager.LoadScene("Boot");
            //StartCoroutine(ExitLoad());
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Boot"));
            //ScenesManager.Instance.UnLoadLevel("IntroduccionMottisTestOscar");
        }

        exit = false;
    }

    IEnumerator ExitLoad()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => !ScenesManager.Instance.isUnLoad);
        ScenesManager.Instance.isLoad = true;
        yield return new WaitForSeconds(10f);
        ScenesManager.Instance.LoadLevel("Introduccion_Mottis"); 
        Debug.Log(222220);
    }

    IEnumerator EnabledRules()
    {
        StatesManager.Instance.rulesPanel.SetActive(true);
        videoPlayer.Play();
        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        yield return new WaitForSeconds(1.5f);
        StatesManager.Instance.rulesPanel.SetActive(false);
        StatesManager.Instance.InGame = true;
        StatesManager.Instance.IsHereSomeOne = false;
        exit = true;
    }
}
