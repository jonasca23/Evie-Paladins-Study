using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvieM2SkillState : EvieBaseState
{
    float skillDuration;

    public EvieM2SkillState(EvieStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.InputHandler.M2PressedEvent += ExitCubeState;

        stateMachine.transform.eulerAngles = Vector3.zero;
        skillDuration = stateMachine.CubeFormDuration;

        stateMachine.ToggleStaff(false);
        stateMachine.IceCube.SetActive(true);
        stateMachine.IceCubeCamera.SetActive(true);
    }

    public override void Tick(float deltaTime)
    {
        skillDuration -= deltaTime;
        Fall(2);

        if(skillDuration <= 0)
        {
            ExitCubeState();
        }
    }
    
    public override void ExitState()
    {
        stateMachine.InputHandler.M2PressedEvent -= ExitCubeState;

        stateMachine.ToggleStaff(true);
        stateMachine.IceCube.SetActive(false);
        stateMachine.IceCubeCamera.SetActive(false);
    }

    void ExitCubeState()
    {
        stateMachine.SwitchState(new EvieStandardState(stateMachine));
    }
}
