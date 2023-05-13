using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicState : State
{
    [Header("States Settings ")]
    [SerializeField]
    bool stateOff;
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

    public override State Tick()
    {
        if (stateOff)
        {
            StopAllCoroutines();
            ExitState();
            return StatesManager.Instance.skipState;
        }

        if (inFirstCinematic)
        {
            StopAllCoroutines();
            Debug.Log(1);
            StartCoroutine(FirstCinematic());
            inFirstCinematic = false;
        }

        if (inSecondCinematic)
        {
            StopAllCoroutines();
            Debug.Log(2);
            StartCoroutine(SecondCinematic());
            inSecondCinematic = false;
        }

        if (inThirdCinematic)
        {
            StopAllCoroutines();
            Debug.Log(3);
            StartCoroutine(ThirdCinematic());
            inThirdCinematic = false;
        }

        if (inFourthCinematic)
        {
            StopAllCoroutines();
            Debug.Log(4);
            StartCoroutine(FourthCinematic());
            inFourthCinematic = false;
        }

        if (inFivethCinematic)
        {
            StopAllCoroutines();
            Debug.Log(5);
            StartCoroutine(FivethCinematic());
            inFivethCinematic = false;
        }

        if (inSixthCinematic)
        {
            StopAllCoroutines();
            Debug.Log(6);
            StartCoroutine(SixthCinematic());
            inSixthCinematic = false;
        }

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
        }
    }

    IEnumerator FirstCinematic()
    {
        SceneController.Instance.timeLineRutine.Play(0);
        StatesManager.Instance.skapeTask.ChangeSize(false);

        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);
        StatesManager.Instance.skapeTask.RestartSize(false);

        yield return new WaitUntil(() => !SceneController.Instance.timeLineRutine.StatePlayable(0));
        ChangeState(2);
    }

    IEnumerator SecondCinematic()
    {
        SceneController.Instance.timeLineRutine.Play(1);
        yield return new WaitUntil(() => !SceneController.Instance.timeLineRutine.StatePlayable(1));

        //start qte de temporizador de pago.
        //mostrara precio. originalGameObject
        //Decidir si recargar escena nueva o continuar el proceso de venta;
        if (SceneController.Instance != null)
        {
            SceneController.Instance.QTEManager.StartEvent(SceneController.Instance.QTE[0]);

            yield return new WaitForSeconds(5f);
            MottisController.Instance.bodyAnim.SetTrigger("HearSecret");

            yield return new WaitUntil(() => !SceneController.Instance.QTEManager.startEvent);
        }

        if (StatesManager.Instance.PaymentMade)
        {
            SoundManager.Instance.PlayNewSound("SelectedFinish");
            ChangeState(4);
        }
        else
        {
            ChangeState(3);
        }
        
        MottisController.Instance.SetHappy(false);
        MottisController.Instance.SetSmile(false);
        StatesManager.Instance.isShowing = false;
    }

    //Skip to main state
    IEnumerator ThirdCinematic()
    {
        SceneController.Instance.timeLineRutine.Play(2);

        yield return new WaitUntil(() => !SceneController.Instance.timeLineRutine.StatePlayable(2));
        StatesManager.Instance.skipState.exit = true;
        ChangeState(0);

        StatesManager.Instance.skapeTask.ChangeSize(true);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);

        yield return new WaitForSeconds(5f);
    }

    IEnumerator FourthCinematic()
    {
        SceneController.Instance.timeLineRutine.Play(3);
        yield return new WaitUntil(() => !SceneController.Instance.timeLineRutine.StatePlayable(3));
        StatesManager.Instance.ledsController.ramdom = false;

        if (SceneController.Instance != null)
        {
            SceneController.Instance.QTEManager.StartEvent(SceneController.Instance.QTE[1]);
        }
        Debug.Log("next Part 1");
    }

    public IEnumerator SendResultsAcceptPlay()
    {
        yield return new WaitUntil(() => !SceneController.Instance.QTEManager.startEvent);
        yield return new WaitUntil(() => SceneController.Instance.setBoolStartGame);

        StatesManager.Instance.ledsController.ramdom = true;

        yield return new WaitForSeconds(1f);
        StatesManager.Instance.ui.questPanel.SetActive(false);

        SoundManager.Instance.PlayNewSound("SelectedFinish");

        // validar si acepta o no el reto para definir acciones

        if (StatesManager.Instance.ChallengeAccepted)
        {
            ChangeState(5);
        }
        else
        {
            ChangeState(6);
        }

    }

    IEnumerator FivethCinematic()
    {
        yield return new WaitForSeconds(1f);
        SceneController.Instance.timeLineRutine.Play(4);
        yield return new WaitUntil(() => !SceneController.Instance.timeLineRutine.StatePlayable(4));
        StatesManager.Instance.skipState.isRulesTime = true;
        StatesManager.Instance.InCinematic = false;
        ChangeState(0);
        yield return new WaitForSeconds(2f);
    }

    //Skip to main state
    IEnumerator SixthCinematic()
    {
        SceneController.Instance.timeLineRutine.Play(5);
        yield return new WaitUntil(() => !SceneController.Instance.timeLineRutine.StatePlayable(5));
        yield return new WaitForSeconds(2f);
        StatesManager.Instance.skapeTask.RestartSize(false);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);
        StatesManager.Instance.skipState.exit = true;
        ChangeState(0);
    }

    public override void ExitState()
    {
        StopAllCoroutines();
        stateOff = false;
        StatesManager.Instance.PaymentMade = false;
        StatesManager.Instance.ChallengeAccepted = false;
        SceneController.Instance.setBoolStartGame = false;
    }
}
