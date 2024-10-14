using Godot;
using System;

public partial class StateManager : Control
{

    /* LISTA DE ADIÇÕES:
    - VARIACOES DA PUPILA (HARD) (ESQUERDA, DIREITA, MEIO)
    - VARIACOES DO ROSTO (SOFT)
    - HORARIO DO DIA MUDA BACKGROUND
    - VARIACOES DE ENERGIA
    */
    [Export] public AnimatedSprite2D animation;

    BaseState currentState;

    // INSTACIA AS CLASSES (ESTADOS)
    public TristeState tristeState = new TristeState();
    public MedoState medoState = new MedoState();


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

    public void SwitchVariant(){

    }
}
