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
        if (Input.IsActionJustPressed("Esquerda-meio"))
		{
			State.SwitchState(State.tristeState);
		}
		else if (Input.IsActionJustPressed("NextInput"))
		{
			State.SwitchState(State.tristeState);
		}  
    }

    public override void LeaveState(StateManager State)
    {
        
    }
}
