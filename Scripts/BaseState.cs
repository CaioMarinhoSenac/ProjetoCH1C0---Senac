using Godot;

public partial class BaseState : AnimatedSprite2D
{
    public virtual void EnterState(StateManager State)
    {
        State.faceAnimation.Play(this.GetType().Name);
    }
    public virtual void UpdateState(StateManager State)
    {

    }
    public virtual void LeaveState(StateManager State)
    {

    }
}