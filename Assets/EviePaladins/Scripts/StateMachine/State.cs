public abstract class State 
{
    public abstract void EnterState();
    public abstract void Tick(float deltaTime);
    public abstract void ExitState();
}
