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
			State.SwitchState(State.inicialState);
		}
		if (Input.IsActionJustPressed("NextInput"))
		{
			State.SwitchState(State.inicialState);
		}
	}

	public override void LeaveState(StateManager State)
	{
		
	}
}
