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

    public void SwitchColor(Sprite2D target, Color targetColor, float speed, bool loop)
    {
        if(!loop)
        {
            Tween tween = target.CreateTween().SetLoops(0);

            tween.TweenProperty(target, "modulate", targetColor, speed);
        }
        else
        {
            Tween tween = target.CreateTween().SetLoops();

            tween.TweenProperty(target, "modulate", new Color(1, 0.65f, 0, 0.5f), 0.75f);
            tween.TweenProperty(target, "modulate", new Color(1.0f, 0.41f, 0.71f, 0.5f), 0.75f);
            tween.TweenProperty(target, "modulate", new Color(0.0f, 1.0f, 0.0f, 0.5f), 0.75f);
        }
    }
}
