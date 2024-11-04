using Godot;
using System;

public partial class PiscandoState : BaseState
{
    Tween tween;
    public override void SwitchCircuitColor(Color targetColor)
    {
        tween = CreateTween().SetLoops();

        tween.TweenProperty(StateManager.CircuitColor, "modulate", new Color(1, 0.65f, 0, 0.5f), 0.75f);
        tween.TweenProperty(StateManager.CircuitColor, "modulate", new Color(1.0f, 0.41f, 0.71f), 0.75f);
        tween.TweenProperty(StateManager.CircuitColor, "modulate", new Color(0.0f, 1.0f, 0.0f), 0.75f);
    }

    public override void LeaveState(StateManager State)
    {
        base.LeaveState(State);

        tween.Free();
    }
}
