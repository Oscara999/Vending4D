using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisController : MonoBehaviour
{
    [SerializeField]Transform camera;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    private void Update()
    {
    }

    public void SetSpeakingBool(bool state)
    {
        Debug.Log("Speaking " + state);
        anim.SetBool("IsTalking", state);
    }

    public void SetMovingBool(bool state)
    {
        anim.SetBool("IsMoving", state);
    }

    public void SetIndex(int index)
    {
        anim.SetInteger("Index", index);
    }

    public void SetBlinkBool(bool state)
    {
        anim.SetBool("IsBlinking", state);
    }
}
