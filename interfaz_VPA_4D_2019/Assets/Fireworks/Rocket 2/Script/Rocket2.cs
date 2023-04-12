﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket2 : MonoBehaviour
{

    public Rigidbody rig;
    public ConstantForce cf;
    public Transform IsKinematic;

    IEnumerator Start()

    {
        //Wait for 3 secs.
        yield return new WaitForSeconds(1.6f);

        //Game object will turn off
        GameObject.Find("MeshRenderer2").SetActive(false);

        rig.isKinematic = true;
        cf.enabled = false;


    }
}
