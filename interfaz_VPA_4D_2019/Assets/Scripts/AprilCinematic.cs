using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AprilCinematic : MonoBehaviour
{
    [SerializeField]
    Animator aprilAnim;
    // Start is called before the first frame update
    void Start()
    {
        aprilAnim = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void SetFly(bool state)
    {
        aprilAnim.SetBool("isFly", state);
    }
    public void SetIdle(bool state)
    {
        aprilAnim.SetBool("isIdle", state);
    }
    public void SetJump(bool state)
    {
        aprilAnim.SetBool("isFly", state);
    }
    public void SetGround(bool state)
    {
        aprilAnim.SetBool("isGround", state);
    }
}
