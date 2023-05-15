using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;

//[DefaultExecutionOrder(-1)]
public class ManagerGame : Singleton<ManagerGame>
{
    public QTEManager QTEManager;
    public Timer timer;
    public int round;
    bool inProcess;
    public GameObject timeLines;
    public TimeLineRutine timeLineRutine;

    void Start()
    {
        SetChangeTimeLine(timeLines);
        StartCoroutine(StartGame());
        StatesManager.Instance.ui.uIController.kindScene = kindScene.Game;
    }
    void SetChangeTimeLine(GameObject container)
    {
        if (timeLineRutine != null)
        {
            timeLineRutine.SetCinematics(container);
        }
    }

    public void Update()
    {
        if (Player.Instance.IsActivate)
        {
            //HandsState();
            DamagePlayerUI();
            DamageEnemyUI();
        }
    }

    void HandsState()
    {
        if (!StatesManager.Instance.hands[0].activeInHierarchy && inProcess || !StatesManager.Instance.hands[1].activeInHierarchy && inProcess)
        {
            if (!ScenesManager.Instance.IsPaused)
            {
                PauseGame();
            }
            return;

        }
        else
        {
            if (ScenesManager.Instance.IsPaused)
            {
                PauseGame();
            }
        }
    }

    public void ChageRoud()
    {
        if (round == 0 || round == 1)
        {
            round += 1;
            Enemy.Instance.SetOrderWaitPoints(round);
        }
    }

    IEnumerator StartGame()
    {
        timeLineRutine.Play(0);

        StatesManager.Instance.skapeTask.ChangeSize(false);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);

        StatesManager.Instance.skapeTask.RestartSize(false);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);
        yield return new WaitUntil(() => !timeLineRutine.StatePlayable(0));

        StatesManager.Instance.ui.GamePanel.SetActive(true);

        //Debug.Log("1");
        Player.Instance.StateController();
        //Debug.Log("2");
        Enemy.Instance.IsActivate = true;
        //Enemy.Instance.ChageStateAnimation();
        inProcess = true;
        timer.StartTimer();
    }

    public void PauseGame()
    {
        ScenesManager.Instance.Pause();
    }

    public void DamageEnemyUI()
    {
        if (Enemy.Instance.healt.health > 0)
        {
            StatesManager.Instance.ui.sliderEnemyUI.gameObject.SetActive(true);
            StatesManager.Instance.ui.sliderEnemyUI.value = Enemy.Instance.healt.health;
        }
        else
        {
            StatesManager.Instance.ui.sliderEnemyUI.gameObject.SetActive(false);
        }
    }

    public void DamagePlayerUI()
    {
        switch (Player.Instance.lifes)
        {
            case 0:
                StatesManager.Instance.ui.lifesUI[0].gameObject.GetComponent<Animator>().SetBool("Damage", true);
                FinishGame();
                break;
            case 1:
                StatesManager.Instance.ui.lifesUI[1].gameObject.GetComponent<Animator>().SetBool("Damage", true);
                break;
            case 2:
                StatesManager.Instance.ui.lifesUI[2].gameObject.GetComponent<Animator>().SetBool("Damage", true);
                break;
        }
    }

    public void FinishGame()
    {
        //StatesManager.Instance.uIController.CrossFireState(false);
        StatesManager.Instance.ui.sliderEnemyUI.gameObject.SetActive(false);
        Enemy.Instance.IsActivate = false;
        Enemy.Instance.ChageStateAnimation();
        Player.Instance.StateController();
        Player.Instance.gameObject.SetActive(false);
        StopAllCoroutines();
        //GameUI[2].SetActive(true);
        inProcess = false;
        StatesManager.Instance.skipState.exit = true;
        StatesManager.Instance.currentState = StatesManager.Instance.skipState;
    }
}
