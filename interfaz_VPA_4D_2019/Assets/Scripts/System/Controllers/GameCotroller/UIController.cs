using UnityEngine;
using Leap.Unity;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.Events;
using System;
using System.Collections;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class UIController : MonoBehaviour
{
    [Header("Moving Cursor Settings")]
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField] RiggedHand HandModelBase;
    [SerializeField]
    float speed;
    Mouse virtualMouse;
    Mouse currentMouse;

    [Header("States")]
    public bool enabledMovement;
    public  Vector2 ScreenXy;
    public Ray ray;
    public Camera positionReferenceCamera;

    [Header("Setup UI")]
    public float currentTime;
    public float smoothTimeUpdate;
    public float time;

    void OnEnable()
    {
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
        if (virtualMouse != null && virtualMouse.added)
        {
            playerInput.user.UnpairDevice(virtualMouse);
            InputSystem.RemoveDevice(virtualMouse);
        }
    }

    void FixedUpdate()
    {
        if (virtualMouse == null || !enabledMovement)
            return;
        CursorMovement();
        VirtualMouseMovement();
    }
    public Vector2 VirtualMousePosition()
    {
        return virtualMouse.position.ReadValue();
    }
    void VirtualMouseMovement()
    {
        //Moving virtual mouse
        Vector2 currentPosition = Vector3.zero;
        Vector2 newPosition = currentPosition + ScreenXy;

        //Moving virtual mouse
        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, ScreenXy);
        
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

            ScreenXy = positionReferenceCamera.WorldToScreenPoint(handPostition * speed);
        }
        else
        {
            if (!StatesManager.Instance.ui.DesingCursor[1].activeInHierarchy)
            {
                StatesManager.Instance.ui.DesingCursor[0].SetActive(false);
                StatesManager.Instance.ui.DesingCursor[1].SetActive(true);
            }
            
            ray = Camera.main.ScreenPointToRay(StatesManager.Instance.ui.crossFire.transform.position);
            ScreenXy = Camera.main.WorldToScreenPoint(handPostition * speed);
            Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red);
        }
    }

    void AnchorCursor(Vector2 position)
    {
        StatesManager.Instance.ui.crossFire.transform.position = new Vector3(position.x, position.y, StatesManager.Instance.ui.crossFire.transform.position.z);
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
