using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    public bool startGame;

    public override State Tick()
    {
        //if (startGame)
        //{
        //    StartCoroutine(StartMachine());
        //    startGame = false;
        //}

        //if (ManagerGame.Instance == null)
        //    return this;

        //ManagerGame.Instance.UpdateState();
        return this;
    }
    IEnumerator StartMachine()
    {
        StatesManager.Instance.skapeTask.ChangeSize(false);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);
    }

    public override void ExitState()
    {
        StatesManager.Instance.RestartMachine();
    }
}
