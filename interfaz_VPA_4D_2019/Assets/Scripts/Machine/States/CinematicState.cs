using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicState : State
{
    [Header("Next States")]
    public SkipState skipState;

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
        if (StatesManager.Instance.InCinematic
            || ScenesManager.Instance.isLoad)
            return this;

        if (stateOff)
        {
            ExitState();
            return skipState;
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
        StatesManager.Instance.timeLineRutine.Play(0);
        StatesManager.Instance.skapeTask.ChangeSize(false);
        StatesManager.Instance.StateInCinematic(true);

        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);
        StatesManager.Instance.skapeTask.RestartSize(false);

        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(0));
        ChangeState(2);
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator SecondCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(1);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(1));
        
        //start qte de temporizador de pago. mostrara precio. originalGameObject;
        if (SceneController.Instance != null)
        {
            QTEManager.Instance.StartEvent(SceneController.Instance.QTE.transform.GetChild(0).gameObject.GetComponent<QTEEvent>());

            yield return new WaitForSeconds(5f);
            StatesManager.Instance.Recharge(1);

            yield return new WaitUntil(() => !QTEManager.Instance.startEvent);
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

        StatesManager.Instance.StateInCinematic(false);
    }

    //Skip to main state
    IEnumerator ThirdCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(2);
        
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(2));
        skipState.exit = true;
        ChangeState(0);

        StatesManager.Instance.skapeTask.ChangeSize(true);
        yield return new WaitUntil(() => !StatesManager.Instance.skapeTask.start);
        
        yield return new WaitForSeconds(5f);
        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator FourthCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(3);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(3));
        StatesManager.Instance.ledsController.ramdom = false;

        if (SceneController.Instance != null)
        {
            QTEManager.Instance.StartEvent(SceneController.Instance.QTE.transform.GetChild(1).gameObject.GetComponent<QTEEvent>());
            yield return new WaitUntil(() => !QTEManager.Instance.startEvent);
        }

        yield return new WaitUntil(() => SceneController.Instance.setBoolStartGame);

        StatesManager.Instance.ledsController.ramdom = true;

        yield return new WaitForSeconds(1f);
        StatesManager.Instance.questPanel.SetActive(false);

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

        StatesManager.Instance.StateInCinematic(false);
    }

    IEnumerator FivethCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(4);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(4));
        skipState.isRulesTime = true;
        ChangeState(0);
        yield return new WaitForSeconds(2f);
        StatesManager.Instance.StateInCinematic(false);
    }
    
    //Skip to main state
    IEnumerator SixthCinematic()
    {
        StatesManager.Instance.StateInCinematic(true);
        StatesManager.Instance.timeLineRutine.Play(5);
        yield return new WaitUntil(() => !StatesManager.Instance.timeLineRutine.StatePlayable(5));
        yield return new WaitForSeconds(2f);
        skipState.exit = true;
        ChangeState(0);
        StatesManager.Instance.StateInCinematic(false);
    }

    protected override void ExitState()
    {
        StopAllCoroutines();
        stateOff = false;
        StatesManager.Instance.PaymentMade = false;
        StatesManager.Instance.ChallengeAccepted = false;
        SceneController.Instance.setBoolStartGame = false;
        StatesManager.Instance.StateInCinematic(false);
    }
}
