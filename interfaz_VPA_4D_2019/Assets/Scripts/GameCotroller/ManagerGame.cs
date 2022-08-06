using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[DefaultExecutionOrder(-1)]
public class ManagerGame : Singleton<ManagerGame>
{
    public Slider sliderEnemyUI;
    public Image[] lifesUI;
    public GameObject[] GameUI;
    public Timer timer;
    public int round;
    bool inProcess;

    void Start()
    {
        StartCoroutine(StartGame());
        StatesManager.Instance.uIController.kindScene = kindScene.Game;
    }

    public void Update()
    {
        if (Player.Instance.IsActivate)
        {
            HandsState();
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
        Debug.Log(223232);
        
        yield return new WaitForSeconds(5f);
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
                lifesUI[0].gameObject.SetActive(false);
                lifesUI[1].gameObject.SetActive(false);
                lifesUI[2].gameObject.SetActive(false);

                FinishGame();
                break;
            case 1:
                lifesUI[0].gameObject.SetActive(true);
                lifesUI[1].gameObject.SetActive(false);
                lifesUI[2].gameObject.SetActive(false);
                break;
            case 2:
                lifesUI[0].gameObject.SetActive(true);
                lifesUI[1].gameObject.SetActive(true);
                lifesUI[2].gameObject.SetActive(false);
                break;
            case 3:
                lifesUI[0].gameObject.SetActive(true);
                lifesUI[1].gameObject.SetActive(true);
                lifesUI[2].gameObject.SetActive(true);
                break;
        }
    }

    public void FinishGame()
    {
        StatesManager.Instance.uIController.CrossFireState(false);
        sliderEnemyUI.gameObject.SetActive(false);
        Enemy.Instance.IsActivate = false;
        Enemy.Instance.ChageStateAnimation();
        Player.Instance.StateController();
        Player.Instance.gameObject.SetActive(false);
        GameUI[2].SetActive(true);
        inProcess = false;
    }
}
