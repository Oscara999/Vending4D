using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicState : State
{
    [Header("States ")]
    public GameState gameState;
    public SkipState skipState;
    public PaymentValidationProcess validationProcessState;
    [SerializeField]
    State nextState;

    [Header("States Settings ")]
    [SerializeField]
    bool stateOff;
    [SerializeField]
    bool skipCinematic;
    [SerializeField]
    bool inFirstCinematic;
    [SerializeField]
    bool inSecondCinematic;
    [SerializeField]
    bool inThirdCinematic;
    [SerializeField]
    bool inFourthCinematic;
    [SerializeField]
    bool inFivethCinematic;
    [SerializeField]
    bool inSixthCinematic;
    [SerializeField]
    bool inSeventhCinematic;

    public override State Tick()
    {
        if (StatesManager.Instance.InCinematic)
            return this;

        if (stateOff)
        {
            Debug.Log("exit");
            ExitState();
            return nextState;
        }

        if (inFirstCinematic)
        {
            Debug.Log(1);
            StartCoroutine(FirstCinematic());
            inFirstCinematic = false;
        }

        if (inSecondCinematic)
        {
            Debug.Log(2);
            StartCoroutine(SecondCinematic());
            inSecondCinematic = false;
        }

        if (inThirdCinematic)
        {
            Debug.Log(3);
            StartCoroutine(ThirdCinematic());
            inThirdCinematic = false;
        }

        if (inFourthCinematic)
        {
            Debug.Log(4);
            StartCoroutine(FourthCinematic());
            inFourthCinematic = false;
        }

        if (inFivethCinematic)
        {
            Debug.Log(5);
            StartCoroutine(FivethCinematic());
            inFivethCinematic = false;
        }

        if (inSixthCinematic)
        {
            Debug.Log(6);
            StartCoroutine(SixthCinematic());
            inSixthCinematic = false;
        }

        if (inSeventhCinematic)
        {
            Debug.Log(7);
            StartCoroutine(SeventhCinematic());
            inSeventhCinematic= false;
        }

        Debug.Log(00);
        return this;
    }

    public void ChangeState(int index)
    {
        switch (index)
        {
            case 0:
                stateOff = true;
                break;

            case 1:
                inFirstCinematic = true;
                break;

            case 2:
                inSecondCinematic = true;
                break;

            case 3:
                inThirdCinematic = true;
                break;

            case 4:
                inFourthCinematic = true; 
                break;

            case 5:
                inFivethCinematic = true;
                break;

            case 6:
                inSixthCinematic = true;
                break;

            case 7:
                inSeventhCinematic = true;
                break;
        }
    }

    IEnumerator FirstCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(0);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(0));
        ChangeState(2);
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator SecondCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(1);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(1));
        //start temporizador de pago y mostrar precio 
        yield return new WaitForSeconds(10f);
        // if para validar si seguir o salir 
        
        if (StatesManager.Instance.SubtractCoin())
        {
            ChangeState(4);
        }
        else
        {
            ChangeState(3);
        }
        
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator ThirdCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(2);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(2));
        ChangeState(0);
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator FourthCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(3);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(3));
        StartCoroutine(SceneController.Instance.StartQuestTimer());
        yield return new WaitForSeconds(10f);

        if (StatesManager.Instance.ChallengeAccepted)
        {
            ChangeState(5);
            // cinematica 5
            //if para validar porque se va y que estado pomer
        }
        else
        {
            ChangeState(6);
            // cinematica 6
            //aqui vooooyyy
        }

        yield return new WaitForSeconds(2f);
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator FivethCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(4);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(4));
        //paso a las reglas
        StatesManager.Instance.InGame = true;
        ChangeState(0);
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator SixthCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(5);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(5));
        ChangeState(7);// play a la cinematica 7
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator SeventhCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(6);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(6));
        skipCinematic = true;
        ChangeState(0);
        StatesManager.Instance.StateInCinematic(false);
    }

    protected override void ExitState()
    {
        if (skipCinematic)
        {
            skipCinematic = false;
            nextState = skipState;
        }

        if (StatesManager.Instance.InGame)
        {
            nextState = gameState;
        }

        stateOff = false;
        StatesManager.Instance.StateInCinematic(false);
    }
}
