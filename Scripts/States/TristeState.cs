using Godot;
using System;

public partial class TristeState : BaseState
{
	public override void EnterState(StateManager State)
	{
		State.faceAnimation.Play("TristeState");
	}

	public override void UpdateState(StateManager State)
	{

	}

	public override void LeaveState(StateManager State)
	{
		
	}
}
