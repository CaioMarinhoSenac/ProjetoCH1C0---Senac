using Godot;
using System;

public partial class VariantManager : AnimatedSprite2D
{
	[Export] public AnimatedSprite2D eyesAnimation;

	[Export] public StateManager stateManager;

	Vector2 originalPosition;

	Tween tween;

	string eyesName;

	float initCooldown = 0f;
	float cooldown = 2.0f; // DELAY ENTRE CADA TROCA DE VARIANTE, PARA DIMINUIR FRENETICIDADE

	float moveSpeed = 0.5f;

	float moveDistance = 15.0f; // Ajuste o valor conforme necessário

	public override void _Ready()
	{
		tween = GetTree().CreateTween();

		originalPosition = eyesAnimation.Position;

		// Iniciar com a posição original
		EyesToDirection(originalPosition);
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
			eyesAnimation.Play(variantName);
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
			targetPosition.X -= moveDistance;
			targetPosition.Y -= moveDistance;
		}
		else if (Input.IsActionJustPressed("Esquerda-meio"))
		{
			targetPosition.X -= moveDistance;
		}
		else if (Input.IsActionJustPressed("Esquerda-baixo"))
		{
			targetPosition.X -= moveDistance;
			targetPosition.Y += moveDistance;
		}
		//  MEIO
		else if (Input.IsActionJustPressed("Meio-cima"))
		{
			targetPosition.Y -= moveDistance;
		}
		else if (Input.IsActionJustPressed("Meio-baixo"))
		{
			targetPosition.Y += moveDistance;
		}
		//  DIREITA
		else if (Input.IsActionJustPressed("Direita-cima"))
		{
			targetPosition.X += moveDistance;
			targetPosition.Y -= moveDistance;
		}
		else if (Input.IsActionJustPressed("Direita-meio"))
		{
			targetPosition.X += moveDistance;
		}
		else if (Input.IsActionJustPressed("Direita-baixo"))
		{
			targetPosition.X += moveDistance;
			targetPosition.Y += moveDistance;
		}

		if (targetPosition != eyesAnimation.Position)
		{
			EyesToDirection(targetPosition);
		}
	}

	public void EyesToDirection(Vector2 targetPosition)
	{
		// Se já houver um Tween em andamento, remova-o
		if (tween != null && IsInstanceValid(tween))
		{
			tween.Kill(); // Para garantir que não haja animações em andamento
		}

		// Crie um novo Tween
		tween = GetTree().CreateTween();

		tween.TweenProperty(eyesAnimation, "position:x", targetPosition.X, moveSpeed);
		tween.Parallel().TweenProperty(eyesAnimation, "position:y", targetPosition.Y, moveSpeed);
	}
}