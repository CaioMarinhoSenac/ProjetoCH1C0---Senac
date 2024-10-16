using Godot;

public partial class BaseState : AnimatedSprite2D
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
    public override void _Process(double delta)
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
            targetPosition.X = originalPosition.X - moveDistance;
            targetPosition.Y = originalPosition.Y - moveDistance;
        }
        else if (Input.IsActionJustPressed("Esquerda-meio"))
        {
            targetPosition.X = originalPosition.X - moveDistance;
			targetPosition.Y = originalPosition.Y;
        }
        else if (Input.IsActionJustPressed("Esquerda-baixo"))
        {
            targetPosition.X = originalPosition.X - moveDistance;
            targetPosition.Y = originalPosition.Y + moveDistance;
        }
        //  MEIO
        else if (Input.IsActionJustPressed("Meio-cima"))
        {
			targetPosition.X = originalPosition.X;
            targetPosition.Y = originalPosition.Y - moveDistance;
        }
		else if (Input.IsActionJustPressed("Meio-meio"))
        {
			targetPosition.X = originalPosition.X;
            targetPosition.Y = originalPosition.Y;
        }
        else if (Input.IsActionJustPressed("Meio-baixo"))
        {
			targetPosition.X = originalPosition.X;
            targetPosition.Y = originalPosition.Y + moveDistance;
        }
        //  DIREITA
        else if (Input.IsActionJustPressed("Direita-cima"))
        {
            targetPosition.X = originalPosition.X + moveDistance;
            targetPosition.Y = originalPosition.Y - moveDistance;
        }
        else if (Input.IsActionJustPressed("Direita-meio"))
        {
            targetPosition.X = originalPosition.X + moveDistance;
			targetPosition.Y = originalPosition.Y;
        }
        else if (Input.IsActionJustPressed("Direita-baixo"))
        {
            targetPosition.X = originalPosition.X + moveDistance;
            targetPosition.Y = originalPosition.Y + moveDistance;
        }

        if (targetPosition != StateManager.faceAnimation.Position)
        {
            animationsLib.MoveToDirection(StateManager.faceAnimation, targetPosition, ref tween, moveSpeed);
        }
    }
}