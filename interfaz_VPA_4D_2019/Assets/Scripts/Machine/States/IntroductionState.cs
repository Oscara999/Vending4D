using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionState : State
{
    public bool inFirstCinematic;

    public override State Tick()
    {
        if (StatesManager.Instance.InCinematic)
        return this;

        if (inFirstCinematic)
        {
            StartCinematic(0);
        }

        //imiciar primera ciematica & segumda ciematica
        // activar letrero de precio
        // Activar temporizador
        return this;
    }

    void StartCinematic(int index)
    {
        StatesManager.Instance.timeLineRutine.Play(index);
        StatesManager.Instance.StateInCinematic(true);
    }


}
