using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvieFSkillState : EvieBaseState
{
    float skillDuration;

    public EvieFSkillState(EvieStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void EnterState()
    {
        skillDuration = stateMachine.FlyingDuration;
        stateMachine.FlyingCamera.SetActive(true);
        stateMachine.InputHandler.FPressedEvent += FinishFSKill;
    }

    public override void Tick(float deltaTime)
    {
        ControlFlyingBroom();
        Rotate();

        skillDuration -= deltaTime;
        if (skillDuration <= 0)
        {
            stateMachine.SwitchState(new EvieStandardState(stateMachine));
        }
    }

    public override void ExitState()
    {
        stateMachine.InputHandler.FPressedEvent -= FinishFSKill;
        stateMachine.FlyingCamera.SetActive(false);
        stateMachine.RestartFSkillCooldown();
        stateMachine.ForceReceiver.ResetVerticalVelocity();
    }

    void FinishFSKill()
    {        
        stateMachine.SwitchState(new EvieStandardState(stateMachine));
    }

    void ControlFlyingBroom()
    {
        stateMachine.transform.position = Vector3.Lerp(stateMachine.transform.position, (stateMachine.transform.position + stateMachine.transform.forward * stateMachine.FlyingSpeed * Time.deltaTime), .8f);

        float x = stateMachine.InputHandler.MouseDelta.x * 200;
        float y = stateMachine.InputHandler.MouseDelta.y * 200;

        Vector3 rotationDirection = (Vector3.forward * y) + (Vector3.up * x);
        stateMachine.MyCamera.Rotate(rotationDirection);
    }
}