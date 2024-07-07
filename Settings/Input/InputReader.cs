using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
{
    public event Action<char> QAction;
    public event Action<char> WAction;
    public event Action<char> EAction;
    public event Action<char> RAction;
    public event Action CompletionAction;
    public event Action<bool> MoveAction;
    public event Action StopAction;
    public event Action InteractAction;
    public event Action TabAction;
    public event Action PauseAction;

    public Vector3 AimPosition { get; private set; }
    
    private Controls _controls;

    private void OnEnable() {
        if(_controls == null) {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.UI.SetCallbacks(this);
        }
        
        _controls.Player.Enable();
        _controls.UI.Enable();
    }

    public void ToggleControl(bool flag) {
        if(flag) _controls.Player.Enable();
        else _controls.Player.Disable();
    }

    public void OnQ(InputAction.CallbackContext context) {
        if(context.performed)
            QAction?.Invoke('Q');
    }

    public void OnW(InputAction.CallbackContext context) {
        if(context.performed)
            WAction?.Invoke('W');
    }

    public void OnE(InputAction.CallbackContext context) {
        if(context.performed)
            EAction?.Invoke('E');
    }

    public void OnR(InputAction.CallbackContext context) {
        if(context.performed)
            RAction?.Invoke('R');
    }

    public void OnCompletion(InputAction.CallbackContext context) {
        if(context.performed)
            CompletionAction?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context) {
        if(context.performed)
            MoveAction?.Invoke(true);
        else if(context.canceled)
            MoveAction?.Invoke(false);
    }

    public void OnAimPosition(InputAction.CallbackContext context) {
        AimPosition = context.ReadValue<Vector2>();
    }

    public void OnStop(InputAction.CallbackContext context) {
        if(context.performed)
            StopAction?.Invoke();
    }

    public void OnInteraction(InputAction.CallbackContext context) {
        if(context.performed)
            InteractAction?.Invoke();
    }

    public void OnMinimapExp(InputAction.CallbackContext context) {
        if(context.performed)
            TabAction?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
            PauseAction?.Invoke();
    }
}
