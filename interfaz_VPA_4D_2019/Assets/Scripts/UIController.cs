using UnityEngine;
using Leap.Unity;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.Events;
using System;
using System.Collections;

[DefaultExecutionOrder(10)]
public class UIController : MonoBehaviour
{
    public bool enabledMovement;
    public Movimiento_UI_Control_Juego movimiento;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    float cursorSpeed;

    Mouse virtualMouse;
    Mouse currentMouse;
    Camera mainCamera;
 
    private void OnEnable()
    {
        mainCamera = Camera.main;
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

        if (StatesManager.Instance.cursorTransform != null)
        {
            Vector2 position = StatesManager.Instance.cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    void OnDisable()
    {
        if (virtualMouse != null && virtualMouse.added)
        {
            playerInput.user.UnpairDevice(virtualMouse);
            InputSystem.RemoveDevice(virtualMouse);
        }

        InputSystem.onAfterUpdate -= UpdateMotion;
    }
    void UpdateMotion()
    {
        if (virtualMouse == null && !enabledMovement)
            return;
        
        movimiento.Selected();
        Vector2 deltaValue = movimiento.ScreenXy;
        Vector2 currentPosition = Vector3.zero; 
        Vector2 newPosition = currentPosition + deltaValue;

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);
        
        AnchorCursor(newPosition);
    }
    
    void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(StatesManager.Instance.canvasRectTransform, position, StatesManager.Instance.canvas.renderMode
            == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition); ;
        StatesManager.Instance.cursorTransform.anchoredPosition = anchoredPosition;
    }
}
