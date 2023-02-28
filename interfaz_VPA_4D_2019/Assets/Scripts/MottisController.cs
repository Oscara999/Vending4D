using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisController : MonoBehaviour
{
    [SerializeField]Transform camera;
    Animator bodyAnim;
    [SerializeField] Animator tailAnim;

    // Start is called before the first frame update
    void Start()
    {
        bodyAnim = GetComponent<Animator>();
    }

    public void Call()
    {
        StartCoroutine(StartAngry());
    }


    private IEnumerator StartAngry()
    {
        yield return new WaitForSeconds(2f);
        bodyAnim.SetTrigger("Angry");
        bodyAnim.SetBool("IsTalking", true);
        bodyAnim.SetBool("IsStress", true);
        tailAnim.SetBool("Angry", true);

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("Molesto 1") &&
            bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        bodyAnim.SetTrigger("Angry");

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("Molesto 1") &&
    bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        bodyAnim.SetBool("IsStress", false);
        bodyAnim.SetBool("IsTalking", false);
        tailAnim.SetBool("Angry", false);

        yield return new WaitForSeconds(3f);
        StartCoroutine(StartHappy());


    }

    private IEnumerator StartHappy()
    {
        yield return new WaitForSeconds(2f);
        bodyAnim.SetBool("IsHappy",true);
        bodyAnim.SetBool("IsTalking", true);
        tailAnim.SetBool("Happy", true);

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("Molesto 1") &&
            bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        bodyAnim.SetTrigger("Angry");

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("Molesto 1") &&
    bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        bodyAnim.SetBool("IsStress", false);
        bodyAnim.SetBool("IsTalking", false);
        tailAnim.SetBool("Angry", false);


        yield return new WaitForSeconds(3f);
        StartCoroutine(StartHappy());

    }


    private IEnumerator StarSad()
    {
        yield return new WaitForSeconds(2f);
        bodyAnim.SetTrigger("Angry");
        bodyAnim.SetBool("IsTalking", true);
        bodyAnim.SetBool("IsStress", true);
        tailAnim.SetBool("Angry", true);

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("Molesto 1") &&
            bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        bodyAnim.SetTrigger("Angry");

        yield return new WaitUntil(() => bodyAnim.GetCurrentAnimatorStateInfo(1).IsName("Molesto 1") &&
    bodyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

        bodyAnim.SetBool("IsStress", false);
        bodyAnim.SetBool("IsTalking", false);
        tailAnim.SetBool("Angry", false);

        yield return new WaitForSeconds(3f);
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

    public void SetBlinkBool(bool state)
    {
        bodyAnim.SetBool("IsBlinking", state);
    }
}
