using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AprilCinematic : MonoBehaviour
{
    Animator aprilAnim;
    Rigidbody aprilRyg;
    
    public GameObject ground;
    public float distance;
    public float distanceMin;
    public float distanceLandMin;
    public bool isGrounded;

    
    // Start is called before the first frame update
    void Start()
    {
        aprilAnim = GetComponent<Animator>();
        aprilRyg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isGrounded)
        {
            distance = Vector3.Distance(transform.position, ground.transform.position);
            
            if (distance < distanceLandMin)
            {
                SetGround(true);
                aprilRyg.useGravity = false;
                aprilRyg.isKinematic = true;
            }
        }
    }

    public void SetIdle(bool state)
    {
        aprilAnim.SetBool("isIdle", state);
    }

    public void SetJump(bool state)
    {
        aprilAnim.SetBool("isJump", state);
    }

    public void SetGround(bool state)
    {
        isGrounded = state;
        aprilAnim.SetBool("isGrounded", state);
    }
}
