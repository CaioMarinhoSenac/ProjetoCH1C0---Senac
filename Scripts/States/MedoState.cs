using Godot;
using System;

public partial class MedoState : BaseState
{
    public override void EnterState(StateManager State)
    {
        State.faceAnimation.Play("MedoState");
    }

    public override void UpdateState(StateManager State)
    {
        
    }

    public override void LeaveState(StateManager State)
    {
        
    }

}
