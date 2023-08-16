using UnityEngine;
using Leap.Unity;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.Events;
using System;
using System.Collections;
using UnityEngine.UI;
using Cinemachine;

[DefaultExecutionOrder(-1)]
public class UIController : MonoBehaviour
{
    [Header("Moving Cursor Settings")]
    [SerializeField]
    PlayerInput playerInput;
    GamePadCursor test;
    [SerializeField] RiggedHand HandModelBase;
    [SerializeField]
    float speed;
    Mouse virtualMouse;
    Mouse currentMouse;

    [Header("States")]
    public bool enabledMovement;
    public  Vector2 screenXy;
    public Ray ray;
    public Camera positionReferenceCamera;
    public GameObject mainCamera;

    [Header("Setup UI")]
    [SerializeField] private Vector2 boundaryMax;
    [SerializeField] private Vector2 boundaryMin;

    public float rotationSpeed = 1.0f;
    public float maxRotationAngle = 10.0f;
    //public bool isMouseOut = false;

    public CinemachineFreeLook freeLookCamera;
    public Vector3 originalRotation;

    void OnEnable()
    {
        test = new GamePadCursor();
        test.Enable();

        //Creating Virtual Mouse
        currentMouse = Mouse.current;

        InputDevice virtualMouseInputDevice = InputSystem.GetDevice("VirtualMouse");

        if (virtualMouseInputDevice == null)
        {
            Debug.Log("start" + currentMouse.name);
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouseInputDevice.added)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else
        {
            virtualMouse = (Mouse)virtualMouseInputDevice;
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

    }

    void OnDisable()
    {
        test.Disable();

        if (virtualMouse != null && virtualMouse.added)
        {
            playerInput.user.UnpairDevice(virtualMouse);
            InputSystem.RemoveDevice(virtualMouse);
        }
    }

    //void FixedUpdate()
    //{
    //    if (virtualMouse == null || !enabledMovement)
    //        return;
    //    CursorMovement();
    //    VirtualMouseMovement();
    //}

    private void LateUpdate()
    {
        if (virtualMouse == null || !enabledMovement)
            return;
        CursorMovement();
        VirtualMouseMovement();
        CameraRotation();
    }

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
            Vector2 mousePosition = screenXy;

            float mouseX = mousePosition.x;
            float mouseY = mousePosition.y;

            // Calcula las rotaciones basadas en el desplazamiento vertical y horizontal del mouse
            float rotationX = mouseY * rotationSpeed;
            float rotationY = mouseX * rotationSpeed;

            //freeLookCamera.m_XAxis.m_InputAxisValue = rotationX;
            //freeLookCamera.m_YAxis.m_InputAxisValue = rotationY;

            Debug.Log("X "+freeLookCamera.m_XAxis.m_InputAxisValue);
            Debug.Log("Y "+freeLookCamera.m_YAxis.m_InputAxisValue);

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

    public Vector2 VirtualMousePosition()
    {
        return virtualMouse.position.ReadValue();
    }

    void VirtualMouseMovement()
    {
        //Moving virtual mouse
        Vector2 currentPosition = Vector3.zero;
        Vector2 newPosition = currentPosition + screenXy;

        //Moving virtual mouse
        InputState.Change(virtualMouse.position, newPosition);

        InputState.Change(virtualMouse.delta, screenXy);
   
        //Moving ui by virtual mouse
        AnchorCursor(virtualMouse.position.ReadValue());
    }

    void CursorMovement()
    {
        if (!StatesManager.Instance.ui.crossFire.activeInHierarchy || !Camera.main)
            return;

        Vector3 handPostition = HandModelBase.GetPalmDirection();
        
        if (StatesManager.Instance.kindScene == KindScene.Menu)
        {
            if (!StatesManager.Instance.ui.DesingCursor[0].activeInHierarchy)
            {
                StatesManager.Instance.ui.DesingCursor[0].SetActive(true);
                StatesManager.Instance.ui.DesingCursor[1].SetActive(false);
            }

            //screenXy = positionReferenceCamera.WorldToScreenPoint(handPostition * speed);
            screenXy = test.UI.Point.ReadValue<Vector2>();

        }
        else
        {
            if (!StatesManager.Instance.ui.DesingCursor[1].activeInHierarchy)
            {
                StatesManager.Instance.ui.DesingCursor[0].SetActive(false);
                StatesManager.Instance.ui.DesingCursor[1].SetActive(true);
            }

            ray = Camera.main.ScreenPointToRay(StatesManager.Instance.ui.crossFire.transform.position);
            screenXy = test.UI.Point.ReadValue<Vector2>();
            //screenXy = Camera.main.WorldToScreenPoint(handPostition * speed);
            Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red);
        }
    }

    void AnchorCursor(Vector2 position)
    {
        if (!Camera.main)
            return;
        
        // Crear un nuevo vecor que almacene la posicion del virtual mouse
        Vector2 screenPoint = new Vector2(position.x, position.y);

        // Obtén las coordenadas de la esquina inferior izquierda de la pantalla en el mundo del juego
        Vector3 minWorldPoint = new Vector3(boundaryMin.x, boundaryMin.y);

        // Obtén las coordenadas de la esquina superior derecha de la pantalla en el mundo del juego
        Vector3 maxWorldPoint = new Vector3(Screen.width - boundaryMax.x, Screen.height - boundaryMax.y);

        // Aplica los límites de la pantalla al movimiento del puntero del mouse
        screenPoint.x = Mathf.Clamp(screenPoint.x, minWorldPoint.x, maxWorldPoint.x);
        screenPoint.y = Mathf.Clamp(screenPoint.y, minWorldPoint.y, maxWorldPoint.y);

        StatesManager.Instance.ui.crossFire.transform.position = new Vector3(screenPoint.x, screenPoint.y, StatesManager.Instance.ui.crossFire.transform.position.z);
    }

    public void CrossFireState(bool isSelected)
    {
        if (isSelected)
        {
            StatesManager.Instance.ui.crossFire.SetActive(true);
        }
        else
        {
            StatesManager.Instance.ui.crossFire.SetActive(false);
        }
    }
}

