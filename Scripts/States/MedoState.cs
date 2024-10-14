using Godot;
using System;

public partial class MedoState : BaseState
{
    public override void EnterState(StateManager State)
    {
        State.animation.Play("MedoState");
    }

    public override void LeaveState(StateManager State)
    {
        
    }

    public override void UpdateState(StateManager State)
    {
        if (Input.IsActionJustPressed("Esquerda-meio"))
		{
			State.SwitchState(State.tristeState);
		}
		if (Input.IsActionJustPressed("NextInput"))
		{
			State.SwitchState(State.tristeState);
		}  
    }

}
