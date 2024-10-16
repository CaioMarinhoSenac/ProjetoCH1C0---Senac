using Godot;
using System;

public partial class VariantManager : AnimatedSprite2D
{
    [Export] public AnimatedSprite2D eyesAnimation;

    [Export] public StateManager stateManager;

	AnimationsLib animationsLib = new AnimationsLib();

    Vector2 originalPosition;

    Tween tween;

    string eyesName;

    float initCooldown = 0f;
    float cooldown = 2.0f; // DELAY ENTRE CADA TROCA DE VARIANTE, PARA DIMINUIR FRENETICIDADE

    float moveSpeed = 0.5f;

    float moveDistance = 30.0f; // Ajuste o valor conforme necessário

    public override void _Ready()
    {
        originalPosition = eyesAnimation.Position;
    }

    public override void _Process(double delta)
    {
        SearchEyes();
        SearchDirection();
    }

    public void SwitchEyes(string variantName)
    {
        if (variantName != null)
        {
            animationsLib.PlayAnimation(eyesAnimation, variantName);
        }
    }

    public void SearchEyes()
    {
        if (stateManager.currentState == stateManager.raivaState)
        {
            eyesName = "RaivaEyes";
        }
        else if (stateManager.currentState == stateManager.felizState)
        {
            eyesName = "FelizEyes";
        }
        else if (stateManager.currentState == stateManager.coracaoState)
        {
            eyesName = "CoracaoEyes";
        }
        else if (stateManager.currentState == stateManager.medoState)
        {
            eyesName = "MedoEyes";
        }

        if (eyesName != null)
        {
            SwitchEyes(eyesName);
        }
    }

    public void SearchDirection()
    {
        Vector2 targetPosition = eyesAnimation.Position;

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

        if (targetPosition != eyesAnimation.Position)
        {
            animationsLib.MoveToDirection(eyesAnimation, targetPosition, ref tween, moveSpeed);
        }
    }
}