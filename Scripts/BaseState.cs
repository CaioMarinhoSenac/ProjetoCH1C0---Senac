using Godot;
using System;

public partial class BaseState : AnimatedSprite
{
    protected AnimationsLib animationsLib = new AnimationsLib();

    protected Vector2 originalPosition;

    protected Tween tween;

    protected float moveSpeed = 0.5f;

    protected float moveDistance = 30.0f; // Ajuste o valor conforme necessário

    protected StateManager StateManager;

    public virtual void EnterState(StateManager State)
    {
        originalPosition = State.faceAnimation.Position;

        StateManager = State;

        SwitchFace();
    }
    public override void _Process(float delta)
    {
        SearchDirection();
    }
    public virtual void LeaveState(StateManager State)
    {

    }

    protected virtual void SwitchFace()
    {
        if (this.GetType().Name != null)
        {
            animationsLib.PlayAnimation(StateManager.faceAnimation, this.GetType().Name);
        }
    }

    protected void SearchDirection()
    {
        Vector2 targetPosition = originalPosition;

        //  ESQUERDA
        if (Input.IsActionJustPressed("Esquerda-cima"))
        {
            targetPosition.x = originalPosition.x - moveDistance;
            targetPosition.y = originalPosition.y - moveDistance;
        }
        else if (Input.IsActionJustPressed("Esquerda-meio"))
        {
            targetPosition.x = originalPosition.x - moveDistance;
			targetPosition.y = originalPosition.y;
        }
        else if (Input.IsActionJustPressed("Esquerda-baixo"))
        {
            targetPosition.x = originalPosition.x - moveDistance;
            targetPosition.y = originalPosition.y + moveDistance;
        }
        //  MEIO
        else if (Input.IsActionJustPressed("Meio-cima"))
        {
			targetPosition.x = originalPosition.x;
            targetPosition.y = originalPosition.y - moveDistance;
        }
		else if (Input.IsActionJustPressed("Meio-meio"))
        {
			targetPosition.x = originalPosition.x;
            targetPosition.y = originalPosition.y;
        }
        else if (Input.IsActionJustPressed("Meio-baixo"))
        {
			targetPosition.x = originalPosition.x;
            targetPosition.y = originalPosition.y + moveDistance;
        }
        //  DIREITA
        else if (Input.IsActionJustPressed("Direita-cima"))
        {
            targetPosition.x = originalPosition.x + moveDistance;
            targetPosition.y = originalPosition.y - moveDistance;
        }
        else if (Input.IsActionJustPressed("Direita-meio"))
        {
            targetPosition.x = originalPosition.x + moveDistance;
			targetPosition.y = originalPosition.y;
        }
        else if (Input.IsActionJustPressed("Direita-baixo"))
        {
            targetPosition.x = originalPosition.x + moveDistance;
            targetPosition.y = originalPosition.y + moveDistance;
        }

        if (targetPosition != StateManager.faceAnimation.Position)
        {
            animationsLib.MoveToDirection(StateManager.faceAnimation, targetPosition, ref tween, moveSpeed);
        }
    }
}
