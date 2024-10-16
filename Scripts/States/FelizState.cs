using Godot;
using System;

public partial class FelizState : BaseState
{
    public override void EnterState(StateManager State)
    {
        State.faceAnimation.Play("FelizState");
    }

    public override void UpdateState(StateManager State)
    {
        
    }

    public override void LeaveState(StateManager State)
    {
        
    }
}
