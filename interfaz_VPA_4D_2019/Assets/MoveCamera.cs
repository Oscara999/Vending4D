using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Vector3 adjustment;

    private void FixedUpdate()
    {
        if (!ManagerGame.Instance.inProcess)
            return;

        if (!StatesManager.Instance.ui.crossFire)
            return;

       transform.position = StatesManager.Instance.ui.crossFire.transform.position + adjustment;
    }
}
