using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvieBaseState : State
{
    protected EvieStateMachine stateMachine;

    public EvieBaseState(EvieStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    protected void Move(float movSpeed)
    {
        Vector3 forward = stateMachine.MyCamera.forward * stateMachine.InputHandler.MovementValue.y;
        forward.y = 0;

        Vector3 right = stateMachine.MyCamera.right * stateMachine.InputHandler.MovementValue.x;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        stateMachine.Controller.Move((forward + right + stateMachine.ForceReceiver.ForceApplied) * Time.deltaTime * movSpeed);
    }

    protected void Fall(float gravityModifier = 1)
    {
        stateMachine.Controller.Move(stateMachine.ForceReceiver.ForceApplied * Time.deltaTime * gravityModifier);
    }

    protected void Rotate()
    {
        float mouseX = stateMachine.InputHandler.MouseDelta.x * Time.deltaTime * stateMachine.CameraSens;
        float mouseY = stateMachine.InputHandler.MouseDelta.y * Time.deltaTime * stateMachine.CameraSens;

        stateMachine.XRotation -= mouseY;
        stateMachine.YRotation += mouseX;

        stateMachine.XRotation = Mathf.Clamp(stateMachine.XRotation, -89f, 89f);

        stateMachine.transform.localRotation = Quaternion.Euler(stateMachine.XRotation, stateMachine.YRotation, 0);
    }
}
