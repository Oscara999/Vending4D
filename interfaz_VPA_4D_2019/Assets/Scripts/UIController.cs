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
    [SerializeField]
    GameObject[] DesingCursor;

    [Header("States")]
    public bool enabledMovement;
    public  Vector2 ScreenXy;
    public kindScene kindScene;
    public GameObject crossFire;
    public Ray ray;
    public Camera positionReferenceCamera;


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

    void Update()
    {
        if (virtualMouse == null || !enabledMovement)
            return;
        Debug.Log("putamadre");

        CursorMovement();
        VirtualMouseMovement();
    }

    void VirtualMouseMovement()
    {
        //Moving virtual mouse
        Vector2 currentPosition = Vector3.zero;
        Vector2 newPosition = currentPosition + ScreenXy;

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, ScreenXy);

        //Moving virtual mouse
        AnchorCursor(virtualMouse.position.ReadValue());
    }

    void CursorMovement()
    {
        if (!crossFire.activeInHierarchy || !Camera.main)
        {
            Debug.Log(3);
            return;
        }

        ray = Camera.main.ScreenPointToRay(crossFire.transform.position);
        Vector3 handPostition = HandModelBase.GetPalmDirection();
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }

        if (kindScene == kindScene.Menu)
        {
            if (!DesingCursor[0].activeInHierarchy)
            {
                DesingCursor[0].SetActive(true);
                DesingCursor[1].SetActive(false);
            }

            ScreenXy = positionReferenceCamera.WorldToScreenPoint(handPostition * speed);
        }
        else
        {
            if (!DesingCursor[1].activeInHierarchy)
            {
                DesingCursor[0].SetActive(false);
                DesingCursor[1].SetActive(true);
            }

            ScreenXy = Camera.main.WorldToScreenPoint(handPostition * speed);
        }
    }

    void AnchorCursor(Vector2 position)
    {
        crossFire.transform.position = new Vector3(position.x, position.y, crossFire.transform.position.z);
        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red);
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
