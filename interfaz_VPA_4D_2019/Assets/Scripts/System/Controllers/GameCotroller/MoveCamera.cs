using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveCamera : MonoBehaviour
{
    public CinemachineVirtualCamera freeLookCamera;
    public CinemachinePOV cinemachineCameraPOV;
    public Vector3 adjustment;

    public GameObject mainCamera;

    public float sencivilidad;
    public float lookspeed;


    private void CameraRotation()
    {
        if (mainCamera == null)
        {
            if (mainCamera != null)
            {
                return;
            }
            else
            {
                mainCamera = GameObject.FindGameObjectWithTag("RotateCamera");

                if (mainCamera)
                {
                    freeLookCamera = mainCamera.GetComponent <CinemachineVirtualCamera>();
                    cinemachineCameraPOV = freeLookCamera.GetCinemachineComponent<CinemachinePOV>();
                }
            }
        }
        else
        {
            //Tarea aqui
            cinemachineCameraPOV.m_HorizontalAxis.Value += StatesManager.Instance.inputController.VirtualMousePosition().x * sencivilidad * lookspeed;

            //freeLookCamera.m_YAxis.Value += StatesManager.Instance.inputController.VirtualMousePosition().y * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!ManagerGame.Instance.inProcess || !StatesManager.Instance.uiController.crossFire)
            return;
        CameraRotation();
        //transform.position = StatesManager.Instance.uiController.crossFire.transform.position + adjustment;
        //transform.position = StatesManager.Instance.inputController.VirtualMousePosition() * rotationSpeed;
    }
}
