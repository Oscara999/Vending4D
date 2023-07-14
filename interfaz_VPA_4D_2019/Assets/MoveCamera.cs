using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform target;
    public float sensitivity = 5f;
    public float maxOffset = 5f;

    private Vector2 virtualMousePosition;
    float mouseX;
    private void Update()
    {
        //// Obtener el movimiento del puntero del mouse en el eje X
        //virtualMousePosition =  StatesManager.Instance.leapMotionMovementController.VirtualMousePosition();
        //mouseX = virtualMousePosition.x;

        //// Calcular el desplazamiento basado en la sensibilidad y el movimiento del mouse
        //float offsetX = mouseX * sensitivity;

        //// Limitar el desplazamiento dentro de los límites establecidos
        //offsetX = Mathf.Clamp(offsetX, -maxOffset, maxOffset);

        //// Calcular la nueva posición de la cámara
        //Vector3 newPosition = target.position + new Vector3(offsetX, 0f, 0f);

        //// Actualizar la posición de la cámara
        //virtualCamera.transform.position = newPosition;
    }
}
