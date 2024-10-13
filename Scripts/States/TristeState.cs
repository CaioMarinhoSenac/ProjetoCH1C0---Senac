using Godot;
using System;

public partial class TristeState : BaseState
{
    public override void EnterState(StateManager State)
    {
        State.animation.Play("TristeState");
    }

    public override void UpdateState(StateManager State)
    {
        return;
    }

    public override void LeaveState(StateManager State)
    {
        
    }
}
