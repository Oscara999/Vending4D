using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicState : State
{
    [Header("States Cinematics")]
    public GameState gameState;
    public bool stateOff;

    [SerializeField]
    bool inFirstCinematic;
    [SerializeField]
    bool inSecondCinematic;
    [SerializeField]
    bool inThirdCinematic;
    private bool inFourthCinematic;

    public override State Tick()
    {
        if (StatesManager.Instance.InCinematic)
            return this;

        if (stateOff)
        {
            ExitState();
            stateOff = false;
            //if para validar porque se va y que estado pomer

            return gameState;
        }

        if (inFirstCinematic)
        {
            StartCoroutine(FirsCinematic());
            inFirstCinematic = false;
        }

        if (inSecondCinematic)
        {
            StartCoroutine(SecondCinematic());
            inSecondCinematic = false;
        }

        if (inThirdCinematic)
        {
            StartCoroutine(ThirdCinematic());
            inThirdCinematic = false;
        }

        if (inFourthCinematic)
        {
            StartCoroutine(FourthCinematic());
            inFourthCinematic = false;
        }

        Debug.Log(00);
        return this;
    }

    public void ChangeState(int index)
    {
        switch (index)
        {
            case 0:
                inFirstCinematic = true;
                break;

            case 1:
                inSecondCinematic = true;
                break;

            case 2:
                inThirdCinematic = true;
                break;
            case 4:
                inFourthCinematic = true; 
                break;

        }
    }

    IEnumerator FirsCinematic()
    {
        StatesManager.Instance.timeLineRutine.Play(0);
        StatesManager.Instance.StateInCinematic(true);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(0));
        ChangeState(1);
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator SecondCinematic()
    {
        StatesManager.Instance.timeLineRutine.Play(1);
        StatesManager.Instance.StateInCinematic(true);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(1));
        //start temporizador de pago y mostrar precio 
        yield return new WaitForSeconds(10f);
        // if para validar si seguir o salir 
        //vamos aquiiiiii
        if (StatesManager.Instance.SubtractCoin())
        {
            ChangeState(3);
        }
        else
        {
            ChangeState(2);
        }

        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator ThirdCinematic()
    {
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(1));
        stateOff = true;
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator FourthCinematic()
    {
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(1));
        StartCoroutine(SceneController.Instance.StartTimer());
        StatesManager.Instance.StateInCinematic(false);
        //validar si acepta o ño el reto
    }
    
    protected override void ExitState()
    {
    }
}
