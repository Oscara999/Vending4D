﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[DefaultExecutionOrder(-1)]
public class ManagerGame : Singleton<ManagerGame>
{
    public QTEManager QTEManager;
    public Slider sliderEnemyUI;
    public Image[] lifesUI;
    public GameObject[] GameUI;
    public Timer timer;
    public int round;
    bool inProcess;

    void Start()
    {
        StartCoroutine(StartGame());
        StatesManager.Instance.ui.uIController.kindScene = kindScene.Game;
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

        StatesManager.Instance.skapeTask.ChangeSize(false);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);

        StatesManager.Instance.skapeTask.RestartSize(false);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);

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
        GameUI[0].SetActive(!GameUI[0].activeInHierarchy);
        ScenesManager.Instance.Pause();
    }

    public void DamageEnemyUI()
    {
        if (Enemy.Instance.healt.health > 0)
        {
            sliderEnemyUI.gameObject.SetActive(true);
            sliderEnemyUI.value = Enemy.Instance.healt.health;
        }
        else
        {
            sliderEnemyUI.gameObject.SetActive(false);
        }
    }

    public void DamagePlayerUI()
    {
        switch (Player.Instance.lifes)
        {
            case 0:
                lifesUI[0].gameObject.GetComponent<Animator>().SetBool("Damage", true);
                FinishGame();
                break;
            case 1:
                lifesUI[1].gameObject.GetComponent<Animator>().SetBool("Damage", true);
                break;
            case 2:
                lifesUI[2].gameObject.GetComponent<Animator>().SetBool("Damage", true);
                break;
        }
    }

    public void FinishGame()
    {
        //StatesManager.Instance.uIController.CrossFireState(false);
        sliderEnemyUI.gameObject.SetActive(false);
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
