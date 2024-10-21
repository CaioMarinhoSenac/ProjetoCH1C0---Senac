using Godot;
using System;

public partial class BaseState : AnimatedSprite
{
    protected AnimationsLib animationsLib = new AnimationsLib();

    protected Tween tween;

    protected float moveSpeed = 0.5f;

    protected float moveDistance = 10.0f;

    protected StateManager StateManager;

    protected Vector2 originalPosition;

    public virtual void EnterState(StateManager State, Vector2 originalPosition)
    {
        StateManager = State;
        
        this.originalPosition = originalPosition;

        SwitchFace();
    }
    public virtual void LeaveState(StateManager State)
    {

    }

    public virtual void SwitchFace()
    {
        if (this.GetType().Name != null)
        {
            animationsLib.PlayAnimation(StateManager.faceAnimation, this.GetType().Name);
        }
    }

    public void SearchDirection(Vector2 currentPosition)
    {
        Vector2 targetPosition = currentPosition;

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