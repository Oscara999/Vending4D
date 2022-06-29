using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReposeState : State
{
    public override State Tick()
    {
        return this;
    }
}
