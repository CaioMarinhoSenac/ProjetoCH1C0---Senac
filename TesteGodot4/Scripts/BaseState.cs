using Godot;

public abstract class BaseState
{
	public abstract void EnterState(StateManager State);
	public abstract void UpdateState(StateManager State);
	public abstract void LeaveState(StateManager State);
}
