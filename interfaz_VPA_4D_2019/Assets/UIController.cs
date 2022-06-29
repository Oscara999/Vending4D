using UnityEngine;
using Leap.Unity;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class UIController : MonoBehaviour
{
    public Movimiento_UI_Control_Juego move;
    public Vector3 positionHand;
    public bool aButtonIsPressed;

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
    [SerializeField]
    float distance;
    bool previousMouseState; 
    [SerializeField]
    Mouse virtualMouse;
    [SerializeField]
    Mouse currentMouse;
    Camera mainCamera;

    const string gamepadScheme = "Gamepad";
    const string mouseScheme = "KeyBoard&Mouse";
    [SerializeField] string previousControlScheme = "";

    private void OnEnable()
    {
        cursorTransform.gameObject.SetActive(true);

        mainCamera = Camera.main;
        currentMouse = Mouse.current;



        InputDevice virtualMouseInputDevice = InputSystem.GetDevice("VirtualMouse");

        if (virtualMouseInputDevice == null)
        {
            Debug.Log("start"+ currentMouse.name);
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouseInputDevice.added)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else
        {
            virtualMouse = (Mouse)  virtualMouseInputDevice;
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
        //playerInput.onControlsChanged += OnControllerChaged;
    }

    void OnDisable()
    {
        if (virtualMouse != null && virtualMouse.added)
        {
            playerInput.user.UnpairDevice(virtualMouse);
            InputSystem.RemoveDevice(virtualMouse);
        }

        InputSystem.onAfterUpdate -= UpdateMotion;
        //playerInput.onControlsChanged -= OnControllerChaged;
    }

    void UpdateMotion()
    {
        if (virtualMouse == null /* || Gamepad.current == null*/)
            return;

        //if (aButtonIsPressed)
        //{
        //    virtualMouse.CopyState<MouseState>(out var mouseState);
        //    mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
        //    InputState.Change(virtualMouse, mouseState);
        //    aButtonIsPressed = false;
        //}

        //Vector2 deltaValue = Mouse.current.position.ReadValue();
        //Vector2 deltaValue = move.crossFire.transform.position;
        Vector2 deltaValue = move.ScreenXy;

        //Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        //deltaValue *= cursorSpeed * Time.deltaTime;

        //Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 currentPosition = Vector3.zero; 
        Vector2 newPosition = currentPosition + deltaValue;

        //newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
        //newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);
        
        //InputState.Change(virtualMouse.delta, deltaValue);

        //bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();


        //if (previousMouseState != aButtonIsPressed)
        //{
        //    virtualMouse.CopyState<MouseState>(out var mouseState);
        //    mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
        //    InputState.Change(virtualMouse, mouseState);
        //    previousMouseState = aButtonIsPressed;
        //}

        AnchorCursor(newPosition);
    }

    //void OnControllerChaged(PlayerInput input)
    //{
    //    if (playerInput.currentControlScheme == mouseScheme && previousControlScheme != mouseScheme)
    //    {
    //        cursorTransform.gameObject.SetActive(false);
    //        Cursor.visible = true;
    //        currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
    //        previousControlScheme = mouseScheme;
    //        AnchorCursor(currentMouse.position.ReadValue());
    //    }
    //    else if (playerInput.currentControlScheme == gamepadScheme && previousControlScheme != gamepadScheme)
    //    {
    //        cursorTransform.gameObject.SetActive(true);
    //        //Cursor.visible = false;
    //        InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
    //        AnchorCursor(currentMouse.position.ReadValue());
    //        previousControlScheme = gamepadScheme;
    //    }

    //    Debug.Log(previousControlScheme);
    //}

    
    void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode
            == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition); ;
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
