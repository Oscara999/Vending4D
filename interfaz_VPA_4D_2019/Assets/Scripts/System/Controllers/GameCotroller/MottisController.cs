using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisController : Singleton<MottisController>
{
    public Animator bodyAnim;
    public Animator tailAnim;

    [Header("States")]
    public bool isHappy;
    public bool isAngry;
    public bool isSad;

    // Start is called before the first frame update
    void Start()
    {
        bodyAnim = GetComponent<Animator>();
    }

    #region CallsEmotions
    public void CallAngry()
    {
        isAngry = true;
        StartCoroutine(StartAngry());
    }

    public void CallSad()
    {
        isSad = true;
        StartCoroutine(StartSad());
    }

    public void CallHappy()
    {
        isHappy = true;
        StartCoroutine(StartHappy());
    }
    public void EndAngry()
    {
        isAngry = false;
    }

    public void EndHappy()
    {
        isHappy = false;
    }

    public void EndSad()
    {
        isSad = false;
    }

    public void ShowPricePanel()
    {
        StatesManager.Instance.isShowing = true;
        StartCoroutine(StatesManager.Instance?.ShowValuePanel());
    }

    #endregion

    #region Expresions Events
    private IEnumerator StartAngry()
    {
        SetAngry(true);
        SetIsStress(true);

        yield return new WaitUntil(() => !isAngry);

        SetAngry(false);
        SetIsStress(false);
    }

    private IEnumerator StartHappy()
    {
        SetHappy(true);
        
        yield return new WaitUntil(()=> !isHappy);
        
        SetHappy(false);
    }

    private IEnumerator StartSad()
    {
        SetSad(true);

        yield return new WaitUntil(() => !isSad);

        SetSad(false);
    }

    #endregion

    #region Bools

    public void SetSmile(bool state)
    {
        bodyAnim.SetBool("IsSmile", state);
    }

    public void SetHappy(bool state)
    {
        bodyAnim.SetBool("IsHappy", state);
        tailAnim.SetBool("Happy", state);
    }

    public void SetAngry(bool state)
    {
        bodyAnim.SetTrigger("Angry");
    }

    public void SetSad(bool state)
    {
        bodyAnim.SetBool("IsSad", state);
        tailAnim.SetBool("Sad", state);
    }

    public void SetShowProduct(bool state)
    {
        bodyAnim.SetBool("IsShowing", state);
    }

    public void SetIsStress(bool state)
    {
        bodyAnim.SetBool("IsStress", state);
        tailAnim.SetBool("Angry", state);
    }

    public void SetSpeakingBool(bool state)
    {
        bodyAnim.SetBool("IsTalking", state);
    }

    public void SetMovingBool(bool state)
    {
        bodyAnim.SetBool("IsMoving", state);
    }

    public void SetIndex(int index)
    {
        bodyAnim.SetInteger("Index", index);
    }

    public void SetBlinkBool(bool state)
    {
        bodyAnim.SetBool("IsBlinking", state);
    }

    public void TailAngry(bool state)
    {
        tailAnim.SetBool("Angry", state);
    }

    public void TailHappy(bool state)
    {
        tailAnim.SetBool("Happy", state);
    }
    
    public void TailSad(bool state)
    {
        tailAnim.SetBool("Sad", state);
    }

    #endregion
}
