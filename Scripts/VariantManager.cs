using Godot;
using System;

public partial class VariantManager : AnimatedSprite
{
    public AnimatedSprite eyesAnimation;

    public StateManager stateManager;

	AnimationsLib animationsLib = new AnimationsLib();

    Vector2 originalPosition;

    Tween tween;

    string eyesName;

    float cooldown = 2.0f; // DELAY ENTRE CADA TROCA DE VARIANTE, PARA DIMINUIR FRENETICIDADE

    float moveSpeed = 0.25f;

    float moveDistance = 25.0f; // Ajuste o valor conforme necessário

    public override void _Ready()
    {
        eyesAnimation = this;
        stateManager = GetNode<StateManager>("../RostoSprite2D");

        originalPosition = eyesAnimation.Position;
    }

    public override void _Process(float delta)
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

        if (targetPosition != eyesAnimation.Position)
        {
            animationsLib.MoveToDirection(eyesAnimation, targetPosition, ref tween, moveSpeed);
        }
    }
}
