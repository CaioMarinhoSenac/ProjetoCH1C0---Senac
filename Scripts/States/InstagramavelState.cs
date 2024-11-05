using Godot;
using System;

public partial class InstagramavelState : BaseState
{
    public InstagramavelState(){
        stateColor = new Color(1, 0, 0, 0.5f);
    }

    public override void EnterState(StateManager State, VariantManager Variant)
    {
        base.EnterState(State, Variant);

        State.animationPlayer.Stop();
    }

    public override void LeaveState(StateManager State)
    {
        base.LeaveState(State);

        State.animationPlayer.Play("Breathing");
    }
}
