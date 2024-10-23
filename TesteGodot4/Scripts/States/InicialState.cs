using Godot;
using System;

public class InicialState : BaseState
{
	public override void EnterState(StateManager State)
	{
		State.stateLabel.Text = "Estamos no InicialState";
	}

	public override void UpdateState(StateManager State) 
	{
		// Verifica se a "PreviousInput" foi acionada
		if (Input.IsActionJustPressed("PreviousInput"))
		{
			State.SwitchState(State.finalState);
		}

		// Verifica se a "NextInput" foi acionada
		if (Input.IsActionJustPressed("NextInput"))
		{
			State.SwitchState(State.midState);
		}
	} 

	public override void LeaveState(StateManager State)
	{
		GD.Print("Saindo do InicialState...");
	} 
}
