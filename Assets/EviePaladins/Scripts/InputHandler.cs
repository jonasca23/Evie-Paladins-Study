using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, StandardInputActions.IFirstPersonStandardActions
{
    public Vector2 MovementValue { get; private set; }

    public event Action JumpEvent;

    public event Action M1PressedEvent;
    public event Action M1ReleasedEvent;

    public event Action M2PressedEvent;
    public event Action M2ReleasedEvent;

    public event Action QPressedEvent;
    public event Action QReleasedEvent;

    public event Action FPressedEvent;
    public event Action FReleasedEvent;

    public Vector2 MouseDelta { get; private set; }

    StandardInputActions inputActions;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputActions = new StandardInputActions();
        inputActions.FirstPersonStandard.SetCallbacks(this);
        inputActions.FirstPersonStandard.Enable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        JumpEvent?.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        //float x = Mathf.Clamp(value.x, 0, 1);
        //float y = Mathf.Clamp(value.y, 0, 1);
        //MouseDelta = new Vector2(x, y);
        MouseDelta = value;
    }

    public void OnM1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            M1PressedEvent?.Invoke();
        }

        if (context.canceled)
        {
            M1ReleasedEvent?.Invoke();
        }
    }

    public void OnM2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            M2PressedEvent?.Invoke();
        }

        if (context.canceled)
        {
            M2ReleasedEvent?.Invoke();
        }
    }

    public void OnQSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            QPressedEvent?.Invoke();
        }

        if (context.canceled)
        {
            QReleasedEvent?.Invoke();
        }
    }

    public void OnFSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FPressedEvent?.Invoke();
        }

        if (context.canceled)
        {
            FReleasedEvent?.Invoke();
        }
    }
}
