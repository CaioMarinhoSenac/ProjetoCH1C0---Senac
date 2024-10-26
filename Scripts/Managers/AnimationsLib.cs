using Godot;
using System;

public partial class AnimationsLib : Node
{
    public void MoveToDirection(AnimatedSprite2D target, Vector2 targetPosition, float moveSpeed)
    {
        MoveTo(target, targetPosition, moveSpeed);
    }

    public void PlayFrameAnimation(AnimatedSprite2D target, string animationName)
    {
        if (animationName != null)
        {
            target.Play(animationName);
        }
    }

    private void MoveTo(AnimatedSprite2D target, Vector2 targetPosition, float moveSpeed)
    {
        Tween tween = target.CreateTween();

        tween.TweenProperty(target, "position", targetPosition, moveSpeed);
    }

    public void Breathing(AnimationPlayer target)
    {
        target.Play("Breathing");
    }
}
