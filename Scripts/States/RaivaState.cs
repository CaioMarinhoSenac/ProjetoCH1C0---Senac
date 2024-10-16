using Godot;
using System;

public partial class RaivaState : BaseState
{
    public override void EnterState(StateManager State)
    {
        State.faceAnimation.Play("RaivaState");
    }

    public override void UpdateState(StateManager State)
    {

    }

    public override void LeaveState(StateManager State)
    {
        
    }
}
