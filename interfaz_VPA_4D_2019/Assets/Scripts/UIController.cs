using UnityEngine;
using Leap.Unity;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.Events;
using System;
using System.Collections;

public class UIController : MonoBehaviour
{
    public Movimiento_UI_Control_Juego move;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    RectTransform canvasRectTransform;
    [SerializeField]
    GameObject mainPanel;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    RectTransform cursorTransform;
    [SerializeField]
    float cursorSpeed;

    Mouse virtualMouse;
    Mouse currentMouse;
    Camera mainCamera;

    private void OnEnable()
    {
        cursorTransform.gameObject.SetActive(true);

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

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    public IEnumerator  StartTimer()
    {
        yield return new WaitForSeconds(5f);
        mainPanel.SetActive(true);
        
        yield return new WaitForSeconds(10f);
        ManagerSound.Instance.PlayNewSound("SelectedFinish");
        
        yield return new WaitForSeconds(2f);
        ScenesManager.Instance.LoadLevel("Test3");
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
        if (virtualMouse == null)
            return;

        Vector2 deltaValue = move.ScreenXy;
        Vector2 currentPosition = Vector3.zero; 
        Vector2 newPosition = currentPosition + deltaValue;

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);
        
        AnchorCursor(newPosition);
    }
    
    void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode
            == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition); ;
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
