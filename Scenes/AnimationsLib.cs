using Godot;
using System;

public class AnimationsLib : Node
{
    private void MoveTo(AnimatedSprite target, Vector2 targetPosition, float moveSpeed)
    {
        Tween tween = new Tween();
        target.AddChild(tween);

        tween.InterpolateProperty(target, "position:x", target.Position.x, targetPosition.x, moveSpeed);
        tween.InterpolateProperty(target, "position:y", target.Position.y, targetPosition.y, moveSpeed);

        tween.Start();
    }

    public void PlayAnimation(AnimatedSprite target, string animationName)
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
            tween.StopAll();
            tween.QueueFree();
        }
    }

    public void MoveToDirection(AnimatedSprite target, Vector2 targetPosition, ref Tween tween, float moveSpeed)
    {
        KillTween(tween);

        MoveTo(target, targetPosition, moveSpeed);
    }

    public void OnTweenCompleted(Tween tween)
    {
        if (tween != null && IsInstanceValid(tween))
        {
            tween.QueueFree();
        }
    }
}
