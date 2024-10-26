using Godot;
using System;

public partial class DormindoState : BaseState
{
    private AnimationPlayer animationPlayer;
    public override void EnterState(StateManager State)
    {
        base.EnterState(State);

        animationPlayer = State.animationPlayer;

        animationPlayer.Stop();
    }
    public override void LeaveState(StateManager State)
    {
        base.LeaveState(State);

        animationPlayer.Play("Breathing");
    }
}
