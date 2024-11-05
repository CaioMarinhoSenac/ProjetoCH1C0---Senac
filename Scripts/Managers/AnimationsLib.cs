using Godot;
using System;

public partial class AnimationsLib : Node
{
    AnimatedSprite2D target;
    
    Tween tween;

    public AnimationsLib (AnimatedSprite2D target){
        this.target = target;

        tween = target.GetTree().CreateTween();
    }
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
            tween.SetLoops(0);

            tween.TweenProperty(target, "modulate", targetColor, speed);
        }
        else
        {
            tween.SetLoops();

            tween.TweenProperty(target, "modulate", new Color(1, 0.65f, 0), 0.4f);
            tween.TweenProperty(target, "modulate", new Color(1.0f, 0.41f, 0.71f), 0.4f);
            tween.TweenProperty(target, "modulate", new Color(0.0f, 1.0f, 0.0f), 0.4f);
        }
    }
}
