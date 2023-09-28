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
public class InputController : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField]
    PlayerInput userInterface;
    GamePadCursor gamePadInput;
    [SerializeField] 
    RiggedHand moveHandModelBase;

    Mouse virtualMouse;
    Mouse currentMouse;

    [Header("Stats")]
    public float speed;
    public bool enabledMovement;
    public Vector2 screenXy;
    public Ray ray;
    
    void OnEnable()
    {
        gamePadInput = new GamePadCursor();
        gamePadInput.Enable();

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

        InputUser.PerformPairingWithDevice(virtualMouse, userInterface.user);

    }

    void OnDisable()
    {
        gamePadInput.Disable();

        if (virtualMouse != null && virtualMouse.added)
        {
            userInterface.user.UnpairDevice(virtualMouse);
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
        ControlSystem();

        if (virtualMouse == null || !enabledMovement)
            return;
        CursorMovement();
        VirtualMouseMovement();
    }

    void ControlSystem()
    {
        if (gamePadInput.Player.Fire.triggered)
        {
            Player.Instance.Attack();
        }

        if (gamePadInput.Player.Skip.triggered)
        {
            StatesManager.Instance.RestartMachine();
        }

        //Si se preciona action InsertCoin recargaremos 1 coin
        if (gamePadInput.Player.InsertCoin.triggered)
        {
            StatesManager.Instance.Recharge(1);
        }

        if (gamePadInput.Player.Menu.triggered)
        {
            ScenesManager.Instance.Pause();
        }

        if (gamePadInput.Player.Restart.triggered)
        {
            StatesManager.Instance.RestartMachine();
        }

        if (gamePadInput.Player.Damage.triggered)
        {
            if (!StatesManager.Instance.InGame)
                return;

            Enemy.Instance.GetDamage(25);
        }

        if (gamePadInput.Player.Test.triggered)
        {
        }

    }

    public Vector2 VirtualMousePosition()
    {
        return gamePadInput.Player.Look.ReadValue<Vector2>();
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
        //StatesManager.Instance.uiController.MoveCursor(virtualMouse.position.ReadValue());
    }

    void CursorMovement()
    {
        if (!StatesManager.Instance.uiController.crossFire.activeInHierarchy || !Camera.main)
            return;

        Vector3 handPostition = moveHandModelBase.GetPalmDirection();
        
        if (StatesManager.Instance.kindScene == KindScene.Menu)
        {
            if (!StatesManager.Instance.uiController.DesingCursor[0].activeInHierarchy)
            {
                StatesManager.Instance.uiController.DesingCursor[0].SetActive(true);
                StatesManager.Instance.uiController.DesingCursor[1].SetActive(false);
            }

            //screenXy = positionReferenceCamera.WorldToScreenPoint(handPostition * speed);
            screenXy = gamePadInput.UI.Point.ReadValue<Vector2>();

        }
        else
        {
            if (!StatesManager.Instance.uiController.DesingCursor[1].activeInHierarchy)
            {
                StatesManager.Instance.uiController.DesingCursor[0].SetActive(false);
                StatesManager.Instance.uiController.DesingCursor[1].SetActive(true);
            }

            ray = Camera.main.ScreenPointToRay(StatesManager.Instance.uiController.crossFire.transform.position);
            screenXy = gamePadInput.UI.Point.ReadValue<Vector2>();
            //screenXy = Camera.main.WorldToScreenPoint(handPostition * speed);
            Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red);
        }
    }
}

