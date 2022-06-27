using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    protected void Update()
    {
        CurrentState?.Tick(Time.deltaTime);        
    }

    public void SwitchState(State _state)
    {
        CurrentState?.ExitState(); 
        CurrentState = _state;
        CurrentState?.EnterState();
    }
}