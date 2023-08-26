using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Vector3 adjustment;

    [HideInInspector]
    public GameObject mainCamera;

    public float rotationSpeed = 1.0f;
    public float maxRotationAngle = 10.0f;
    //public bool isMouseOut = false;

    public CinemachineFreeLook freeLookCamera;
    public Vector3 originalRotation;

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
                    freeLookCamera = mainCamera.GetComponent<CinemachineFreeLook>();
                    originalRotation = mainCamera.transform.eulerAngles;
                }
            }
        }
        else
        {
            Vector2 mousePosition = StatesManager.Instance.inputController.screenXy;

            float mouseX = mousePosition.x;
            float mouseY = mousePosition.y;

            // Calcula las rotaciones basadas en el desplazamiento vertical y horizontal del mouse
            float rotationX = mouseY * rotationSpeed;
            float rotationY = mouseX * rotationSpeed;

            //freeLookCamera.m_XAxis.m_InputAxisValue = rotationX;
            //freeLookCamera.m_YAxis.m_InputAxisValue = rotationY;

            Debug.Log("X " + freeLookCamera.m_XAxis.m_InputAxisValue);
            Debug.Log("Y " + freeLookCamera.m_YAxis.m_InputAxisValue);

        }


        //Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        //// Calcula el desplazamiento del puntero del mouse desde el centro de la pantalla
        //Vector2 mouseDelta = mousePosition - screenCenter;

        //// Calcula el ángulo de rotación basado en el desplazamiento del mouse
        //float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;

        //// Aplica rotación solo si el puntero del mouse está fuera de la pantalla
        //if (mousePosition.x <= 0 || mousePosition.x >= Screen.width ||
        //    mousePosition.y <= 0 || mousePosition.y >= Screen.height)
        //{
        //    isMouseOut = true;
        //    float rotationAmount = Mathf.Clamp(angle, -maxRotationAngle, maxRotationAngle);
        //    mainCamera.transform.Rotate(Vector3.up, rotationAmount * rotationSpeed * Time.deltaTime);
        //}
        //else if (isMouseOut)
        //{
        //    isMouseOut = false;
        //    mainCamera.transform.rotation = Quaternion.identity; // Restaura la rotación a su estado original
        //}

    }

    private void FixedUpdate()
    {
        if (!ManagerGame.Instance.inProcess || !StatesManager.Instance.uiController.crossFire)
            return;

        //transform.position = StatesManager.Instance.uiController.crossFire.transform.position + adjustment;
        transform.position = StatesManager.Instance.inputController.VirtualMousePosition();
    }
}
