using Godot;
using System;

public class FinalState : BaseState
{
	public override void EnterState(StateManager State)
	{
		State.stateLabel.Text = "Estamos no FinalState";
	}

	public override void UpdateState(StateManager State) 
	{
		// Verifica se a "PreviousInput" foi acionada
		if (Input.IsActionJustPressed("W"))
		{
			State.SwitchState(State.midState);
		}

		// Verifica se a "NextInput" foi acionada
		if (Input.IsActionJustPressed("NextInput"))
		{
			State.SwitchState(State.inicialState);
		}
	} 

	public override void LeaveState(StateManager State)
	{
		GD.Print("Saindo do FinalState...");
	} 
}
