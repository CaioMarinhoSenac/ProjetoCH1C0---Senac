using Godot;
using System;

public class MidState : BaseState
{
    public override void EnterState(StateManager State)
    {
        State.stateLabel.Text = "Estamos no MidState";
    }

    public override void UpdateState(StateManager State) 
    {
        // Verifica se a "PreviousInput" foi acionada
        if (Input.IsActionJustPressed("PreviousInput"))
        {
            State.SwitchState(State.inicialState);
        }

        // Verifica se a "NextInput" foi acionada
        if (Input.IsActionJustPressed("NextInput"))
        {
            State.SwitchState(State.finalState);
        }
    } 

    public override void LeaveState(StateManager State)
    {
        GD.Print("Saindo do MidState...");
    } 
}
