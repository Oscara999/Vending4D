using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    public override State Tick()
    {
        //if (ManagerGame.Instance == null)
        //    return this;
        
        //ManagerGame.Instance.UpdateState();
        return this;
    }

    protected override void ExitState()
    {
        throw new System.NotImplementedException();
    }
}
