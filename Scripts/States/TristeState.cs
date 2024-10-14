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
		if (Input.IsActionJustPressed("PreviousInput"))
		{
			State.SwitchState(State.medoState);
		}
		if (Input.IsActionJustPressed("NextInput"))
		{
			State.SwitchState(State.medoState);
		}
	}

	public override void LeaveState(StateManager State)
	{
		
	}
}
