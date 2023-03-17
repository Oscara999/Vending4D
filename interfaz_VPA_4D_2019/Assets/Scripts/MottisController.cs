using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisController : Singleton<MottisController>
{
    public Transform camera;
    public Animator bodyAnim;
    public Animator tailAnim;

    [Header("States")]
    public bool isHappy;
    
    // Start is called before the first frame update
    void Start()
    {
        bodyAnim = GetComponent<Animator>();
    }

    #region CallsEmotions
    public void CallAngry()
    {
        StartCoroutine(StartAngry());
    }

    public void CallSad()
    {
        StartCoroutine(StartSad());
    }

    public void EndHappy()
    {
        isHappy = false;
    }

    public void CallHappy()
    {
        isHappy = true;
        StartCoroutine(StartHappy());
    }
    #endregion

    #region Expresions Events
    private IEnumerator StartAngry()
    {
        yield return new WaitForSeconds(2f);
        bodyAnim.SetTrigger("Angry");
        SetSpeakingBool(true);
        SetIsStress(true);
        tailAnim.SetBool("Angry", true);

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("Molesto 1") &&
            bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        SetSpeakingBool(false);
        SetIsStress(false);
        tailAnim.SetBool("Angry", false);
    }

    private IEnumerator StartHappy()
    {
        SetHappy(true);
        tailAnim.SetBool("Happy", true);

        yield return new WaitUntil(()=> !isHappy);
        //yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("HappyIdle") &&
        //    bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        SetHappy(false);
        tailAnim.SetBool("Happy", false);
    }
    
    private IEnumerator StartSad()
    {
        yield return new WaitForSeconds(2f);

        SetSad(true);
        tailAnim.SetBool("Sad", true);

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("SadIdle") &&
            bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        SetSad(false);
        tailAnim.SetBool("Sad", false);
    }

    #endregion

    #region Bools

    public void ShowPricePanel()
    {
        StartCoroutine(StatesManager.Instance?.ShowValuePanel(5f));
    }

    public void SetSmile(bool state)
    {
        bodyAnim.SetBool("IsSmile", state);
    }

    public void SetHappy(bool state)
    {
        bodyAnim.SetBool("IsHappy", state);
    }

    public void SetSad(bool state)
    {
        bodyAnim.SetBool("IsSad", state);
    }

    public void SetShowProduct(bool state)
    {
        bodyAnim.SetBool("IsShowing", state);
    }

    public void SetIsStress(bool state)
    {
        bodyAnim.SetBool("IsStress", state);
    }

    public void SetSpeakingBool(bool state)
    {
        Debug.Log("Speaking " + state);
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
