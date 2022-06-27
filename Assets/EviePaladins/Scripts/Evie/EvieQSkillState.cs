using UnityEngine;

public class EvieQSkillState : EvieBaseState
{
    public EvieQSkillState(EvieStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.InputHandler.M2PressedEvent += CancelBlink;
        stateMachine.InputHandler.QReleasedEvent += Blink;
        stateMachine.BlinkPlaceholder.SetActive(true);
    }
    
    public override void Tick(float deltaTime)
    {
        AimBlink();
        Move(stateMachine.StandardMovementSpeed);
        Rotate();
    }

    public override void ExitState()
    {
        stateMachine.InputHandler.M2PressedEvent -= CancelBlink;
        stateMachine.InputHandler.QReleasedEvent -= Blink;
        stateMachine.BlinkPlaceholder.SetActive(false);
    }

    void AimBlink()
    {
        RaycastHit hit;
        if (Physics.Raycast(stateMachine.MyCamera.position, stateMachine.MyCamera.forward, 
            out hit, stateMachine.QSkillDistance))
        {
            stateMachine.BlinkPlaceholder.transform.position = hit.point;
        }
        else
        {
            stateMachine.BlinkPlaceholder.transform.position = stateMachine.MyCamera.position + 
                (stateMachine.MyCamera.forward * stateMachine.QSkillDistance);
        }
    }

    void Blink()
    {
        stateMachine.Controller.enabled = false;
        stateMachine.transform.position = stateMachine.BlinkPlaceholder.transform.position;
        stateMachine.BlinkPlaceholder.SetActive(false);
        stateMachine.Controller.enabled = true;
        stateMachine.RestartQSkillCooldown();

        stateMachine.SwitchState(new EvieStandardState(stateMachine));
    }

    void CancelBlink()
    {
        stateMachine.SwitchState(new EvieStandardState(stateMachine));
    }
}
