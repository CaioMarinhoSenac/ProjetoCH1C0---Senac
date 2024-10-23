using Godot;
using System;

public partial class TristeState : BaseState
{
    public override void EnterState(StateManager State){
        base.EnterState(State);

        State.eyesAnimation.Visible = false;
    }

    public override void LeaveState(StateManager State)
    {
        base.LeaveState(State);

        State.eyesAnimation.Visible = true;
    }
}
