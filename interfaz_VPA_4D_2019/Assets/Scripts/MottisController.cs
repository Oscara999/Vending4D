using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisController : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    public void SetSpeakingBool(bool state)
    {
        anim.SetBool("IsTalking", state);
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
