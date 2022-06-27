using UnityEngine;

public class EvieStandardState : EvieBaseState
{
    public EvieStandardState(EvieStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.InputHandler.M1PressedEvent += Shoot;
        stateMachine.InputHandler.M2PressedEvent += EnterCubeState;
        stateMachine.InputHandler.QPressedEvent += EnterBlinkState;
        stateMachine.InputHandler.JumpEvent += Jump;
        stateMachine.InputHandler.FPressedEvent += EnterFlyingState;
    }

    public override void Tick(float deltaTime)
    {
        Move(stateMachine.StandardMovementSpeed);
        Rotate();
    }

    public override void ExitState()
    {
        stateMachine.InputHandler.M1PressedEvent -= Shoot;
        stateMachine.InputHandler.M2PressedEvent -= EnterCubeState;
        stateMachine.InputHandler.QPressedEvent -= EnterBlinkState;
        stateMachine.InputHandler.JumpEvent -= Jump;
        stateMachine.InputHandler.FPressedEvent -= EnterFlyingState;
    }

    void EnterFlyingState()
    {
        stateMachine.FSkill();
    }

    void EnterBlinkState()
    {
        stateMachine.QSkill();
    }

    void EnterCubeState()
    {
        stateMachine.M2Skill();
    }

    void Jump()
    {
        if (!stateMachine.Controller.isGrounded) return;

        stateMachine.ForceReceiver.AddForce(Vector3.up * stateMachine.JumpForce);
    }

    void Shoot()
    {
        if (!stateMachine.CanUseM1Skill()) return;

        Evie_M1_Projectile proj = MonoBehaviour.Instantiate(stateMachine.M1Projectile, stateMachine.M1ProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Evie_M1_Projectile>();
        Vector3 destination = Vector3.zero;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point - stateMachine.M1ProjectileSpawnPoint.position;
        } else
        {
            destination = ray.GetPoint(1000);
        }

        proj.SetDirection(destination.normalized);
        stateMachine.RestartM1Cooldown();
    }
}
