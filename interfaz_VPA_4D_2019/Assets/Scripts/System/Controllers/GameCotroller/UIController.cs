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
    public kindScene kindScene;
    public GameObject crossFire;
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
        if (!crossFire.activeInHierarchy || !Camera.main)
            return;

        Vector3 handPostition = HandModelBase.GetPalmDirection();
        
        if (kindScene == kindScene.Menu)
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
            
            ray = Camera.main.ScreenPointToRay(crossFire.transform.position);
            ScreenXy = Camera.main.WorldToScreenPoint(handPostition * speed);
            Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red);
        }
    }

    void AnchorCursor(Vector2 position)
    {
        crossFire.transform.position = new Vector3(position.x, position.y, crossFire.transform.position.z);
    }

    public void CrossFireState(bool isSelected)
    {
        if (isSelected)
        {
            crossFire.SetActive(true);
        }
        else
        {
            crossFire.SetActive(false);
        }
    }
}
public enum kindScene
{
    Menu, Game
}
