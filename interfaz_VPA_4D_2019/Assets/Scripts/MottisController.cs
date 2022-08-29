using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisController : Singleton<MottisController>
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    public void ResetSpeakBool(bool state)
    {
        anim.SetBool("Speaking",state);
    }
}
