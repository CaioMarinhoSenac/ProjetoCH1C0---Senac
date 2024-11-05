using Godot;
using System;

public partial class VariantManager : AnimatedSprite2D
{
	public AnimatedSprite2D eyesAnimation, faceAnimation, mouthAnimation;

	[Export] Timer directionCooldown;

	bool canSearchDirection = true;

	[Export] public StateManager stateManager;

	private AnimationPlayer animationPlayer;

	public AnimationsLib animationsLib;

	Vector2 originalPositionEyes, originalPositionFace;

	float moveSpeedEyes = 0.5f, moveSpeedFace = 0.5f, moveSpeedMouth = 0.5f; // Ajusta a velocidade que se move

	float moveDistanceEyes = 50.0f, moveDistanceFace = 25.0f; // Ajusta o quanto se move
	
	public override void _Ready()
	{
		eyesAnimation = this;

		animationsLib = new AnimationsLib(this);

		faceAnimation = stateManager.faceAnimation;
		mouthAnimation = stateManager.mouthAnimation;

		directionCooldown.Timeout += DirectionCooldown_timeout;

		originalPositionEyes = eyesAnimation.Position;
		originalPositionFace = faceAnimation.Position;

		animationPlayer = stateManager.animationPlayer;

		animationPlayer.Play("Breathing");
	}

	public override void _Process(double delta)
	{
		SearchDirection();
	}

	public void SearchDirection()
	{		
		if(stateManager.currentState == stateManager.dormindoState || 
		stateManager.currentState == stateManager.instagramavelState){
			return;
		}
		if(canSearchDirection) {
		Vector2 targetPositionEyes = eyesAnimation.Position;
		Vector2 targetPositionFace = faceAnimation.Position;
		Vector2 targetPositionMouth = mouthAnimation.Position;

		//  ESQUERDA
		if (Input.IsActionJustPressed("Esquerda-cima"))
		{
			targetPositionEyes.X = originalPositionEyes.X - moveDistanceEyes;
			targetPositionEyes.Y = originalPositionEyes.Y - moveDistanceEyes;

			targetPositionFace.X = originalPositionFace.X - moveDistanceFace;
			targetPositionFace.Y = originalPositionFace.Y - moveDistanceFace;
		}
		else if (Input.IsActionJustPressed("Esquerda-meio"))
		{
			targetPositionEyes.X = originalPositionEyes.X - moveDistanceEyes;
			targetPositionEyes.Y = originalPositionEyes.Y;

			targetPositionFace.X = originalPositionFace.X - moveDistanceFace;
			targetPositionFace.Y = originalPositionFace.Y;
		}
		else if (Input.IsActionJustPressed("Esquerda-baixo"))
		{
			targetPositionEyes.X = originalPositionEyes.X - moveDistanceEyes;
			targetPositionEyes.Y = originalPositionEyes.Y + moveDistanceEyes;

			targetPositionFace.X = originalPositionFace.X - moveDistanceFace;
			targetPositionFace.Y = originalPositionFace.Y + moveDistanceFace;
		}
		//  MEIO
		else if (Input.IsActionJustPressed("Meio-cima"))
		{
			targetPositionEyes.X = originalPositionEyes.X;
			targetPositionEyes.Y = originalPositionEyes.Y - moveDistanceEyes;

			targetPositionFace.X = originalPositionFace.X;
			targetPositionFace.Y = originalPositionFace.Y - moveDistanceFace;
		}
		else if (Input.IsActionJustPressed("Meio-meio"))
		{
			targetPositionEyes.X = originalPositionEyes.X;
			targetPositionEyes.Y = originalPositionEyes.Y;

			targetPositionFace.X = originalPositionFace.X;
			targetPositionFace.Y = originalPositionFace.Y;
		}
		else if (Input.IsActionJustPressed("Meio-baixo"))
		{
			targetPositionEyes.X = originalPositionEyes.X;
			targetPositionEyes.Y = originalPositionEyes.Y + moveDistanceEyes;

			targetPositionFace.X = originalPositionFace.X;
			targetPositionFace.Y = originalPositionFace.Y + moveDistanceFace;
		}
		//  DIREITA
		else if (Input.IsActionJustPressed("Direita-cima"))
		{
			targetPositionEyes.X = originalPositionEyes.X + moveDistanceEyes;
			targetPositionEyes.Y = originalPositionEyes.Y - moveDistanceEyes;

			targetPositionFace.X = originalPositionFace.X + moveDistanceFace;
			targetPositionFace.Y = originalPositionFace.Y - moveDistanceFace;
		}
		else if (Input.IsActionJustPressed("Direita-meio"))
		{
			targetPositionEyes.X = originalPositionEyes.X + moveDistanceEyes;
			targetPositionEyes.Y = originalPositionEyes.Y;

			targetPositionFace.X = originalPositionFace.X + moveDistanceFace;
			targetPositionFace.Y = originalPositionFace.Y;
		}
		else if (Input.IsActionJustPressed("Direita-baixo"))
		{
			targetPositionEyes.X = originalPositionEyes.X + moveDistanceEyes;
			targetPositionEyes.Y = originalPositionEyes.Y + moveDistanceEyes;

			targetPositionFace.X = originalPositionFace.X + moveDistanceFace;
			targetPositionFace.Y = originalPositionFace.Y + moveDistanceFace;
		}

		if(stateManager.currentState == stateManager.tristeState){
			targetPositionEyes.Y = originalPositionEyes.Y;
			targetPositionFace.Y = originalPositionFace.Y;
		}

		if (targetPositionEyes != eyesAnimation.Position && targetPositionFace != faceAnimation.Position)
		{
			targetPositionMouth.X = targetPositionFace.X;
			targetPositionMouth.Y = targetPositionFace.Y;

			canSearchDirection = false;

			directionCooldown.Start();
			
			animationsLib.MoveToDirection(eyesAnimation, targetPositionEyes, moveSpeedEyes);
			animationsLib.MoveToDirection(faceAnimation, targetPositionFace, moveSpeedFace);
			animationsLib.MoveToDirection(mouthAnimation, targetPositionMouth, moveSpeedMouth);
		}
		}
	}
	private void DirectionCooldown_timeout()
	{
		canSearchDirection = true;
	}
}
