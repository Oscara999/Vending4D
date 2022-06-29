using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class Test : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    RectTransform canvasRectTransform;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    RectTransform cursorTransform;
    [SerializeField]
    float cursorSpeed;
    [SerializeField]
    float padding = 35f;
    Camera mainCamera;
    public Ray ray;

    [SerializeField]
    Mouse virtualMouse; 
    [SerializeField]
    Mouse currentMouse;
    [SerializeField]
    typeOfMouseInput typeOfMouse;

    const string virtualMouseScheme = "LeapMotion";
    const string mouseScheme = "KeyBoard&Mouse";

    [SerializeField] string previousControlScheme = "";

    void OnEnable()
    {
        cursorTransform.gameObject.SetActive(true);

        mainCamera = Camera.main;
        currentMouse = Mouse.current;

        InputDevice virtualMouseInputDevice = InputSystem.GetDevice("VirtualMouse");

        if (virtualMouseInputDevice == null)
        {
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

        playerInput.onControlsChanged += OnControllerChaged;
    }

    void OnDisable()
    {
        if (virtualMouse != null && virtualMouse.added)
        {
            playerInput.user.UnpairDevice(virtualMouse);
            InputSystem.RemoveDevice(virtualMouse);
        }

        playerInput.onControlsChanged -= OnControllerChaged;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(cursorTransform.transform.position);
        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red);

        //Vector2 deltaValue = Mouse.current.position.ReadValue();
        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue(); ;
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();

        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

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

    void OnControllerChaged(PlayerInput input)
    {
        if (playerInput.currentControlScheme == mouseScheme && previousControlScheme != mouseScheme)
        {
            cursorTransform.gameObject.SetActive(false);
            //Cursor.visible = true;
            currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
            AnchorCursor(currentMouse.position.ReadValue());
            typeOfMouse = typeOfMouseInput.Mouse;
            previousControlScheme = mouseScheme;
        }
        else if (playerInput.currentControlScheme == virtualMouseScheme && previousControlScheme != virtualMouseScheme)
        {
            cursorTransform.gameObject.SetActive(true);
            //Cursor.visible = false;
            InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
            AnchorCursor(currentMouse.position.ReadValue());
            typeOfMouse = typeOfMouseInput.VirtualMouse;
            previousControlScheme = virtualMouseScheme;
        }

        Debug.Log(previousControlScheme);
    }
}

public enum typeOfMouseInput
{
    VirtualMouse,
    Mouse,
}
