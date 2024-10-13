using Godot;
using System;

public partial class StateManager : Control
{
    [Export] public AnimatedSprite2D animation;

    BaseState currentState;

    // INSTACIA AS CLASSES (ESTADOS)
    public TristeState tristeState = new TristeState();

    public override void _Ready()
    {
        // define um 'estado atual' = 'estado inicial'
        currentState = tristeState;
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
