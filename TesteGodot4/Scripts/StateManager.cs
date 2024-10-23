using Godot;
using System;

public partial class StateManager : Control
{
	[Export] public Label stateLabel;

	BaseState currentState;

	// INSTACIA AS CLASSES (ESTADOS)
	public InicialState inicialState = new InicialState();
	public MidState midState = new MidState();
	public FinalState finalState = new FinalState();

	public override void _Ready()
	{
		// define um 'estado atual' = 'estado inicial'
		currentState = inicialState;
		// entra no estado.
		currentState.EnterState(this);
	}

	public override void _Process(double delta)
	{
		currentState.UpdateState(this);
	}

	public void SwitchState(BaseState state)
	{
		// sai do estado anterior
		currentState.LeaveState(this);

		// muda o estado atual
		currentState = state;

		// entra no novo estado
		currentState.EnterState(this);
	}
}
