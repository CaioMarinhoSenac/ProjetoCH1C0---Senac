using Godot;
using System;

public partial class AnimationsLib : Node
{
    private void MoveTo(AnimatedSprite2D target, Vector2 targetPosition, float moveSpeed)
    {
        Tween tween = target.GetTree().CreateTween();
        tween.TweenProperty(target, "position:x", targetPosition.X, moveSpeed);
        tween.Parallel().TweenProperty(target, "position:y", targetPosition.Y, moveSpeed);
    }

    public void PlayAnimation(AnimatedSprite2D target, string animationName)
    {
        if (animationName != null)
        {
            target.Play(animationName);
        }
    }

    public void KillTween(Tween tween)
    {
        if (tween != null && IsInstanceValid(tween))
        {
            tween.Kill();
        }
    }

    public void MoveToDirection(AnimatedSprite2D eyesAnimation, Vector2 targetPosition, ref Tween tween, float moveSpeed)
    {
        KillTween(tween);
        MoveTo(eyesAnimation, targetPosition, moveSpeed);
    }
}
