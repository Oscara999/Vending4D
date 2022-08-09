using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipState : State
{
    public ReposeState reposeState;
    public override State Tick()
    {
        Debug.Log("skip");

        if (!StatesManager.Instance.IsThereSomeone)
        {
            return reposeState;
        }
        else
        {
            StatesManager.Instance.IsThereSomeone = false;
            return this;
        }
    }

    protected override void ExitState()
    {
        throw new System.NotImplementedException();
    }
}
