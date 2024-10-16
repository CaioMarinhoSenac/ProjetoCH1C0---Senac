using Godot;
using System;

public partial class CoracaoState : BaseState
{
    public override void EnterState(StateManager State)
    {
        State.faceAnimation.Play("CoracaoState");
    }

    public override void UpdateState(StateManager State)
    {

    }

    public override void LeaveState(StateManager State)
    {
        
    }
}
