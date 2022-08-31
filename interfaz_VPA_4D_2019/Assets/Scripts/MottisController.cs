using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisController : Singleton<MottisController>
{
    public Animator anim;

    // Start is called before the first frame update
    public void StartSpeaking()
    {
        anim.SetBool("Speaking", true);
    }

    public void ResetSpeakBool(bool state)
    {
        anim.SetBool("Speaking",state);
    }
}
