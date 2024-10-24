using Godot;
using System;

public partial class VariantManager : AnimatedSprite
{
	public AnimatedSprite eyesAnimation, faceAnimation, mouthAnimation;

	Timer directionCooldown;

	bool canSearchDirection = true;

	public StateManager stateManager;

	AnimationsLib animationsLib = new AnimationsLib();

	Vector2 originalPositionEyes, originalPositionFace;

	Tween tween;

	float moveSpeedEyes = 0.5f, moveSpeedFace = 0.5f, moveSpeedMouth = 0.5f; // Ajusta a velocidade que se move

	float moveDistanceEyes = 50.0f, moveDistanceFace = 25.0f; // Ajusta o quanto se move
	
	public override void _Ready()
	{
		eyesAnimation = this;

		stateManager = GetNode<StateManager>("../RostoSprite2D");
		faceAnimation = stateManager.faceAnimation;
		mouthAnimation = stateManager.mouthAnimation;

		directionCooldown = GetNode<Timer>("../DirectionCooldown");
		directionCooldown.Connect("timeout", this, "DirectionCooldown_timeout");

		originalPositionEyes = eyesAnimation.Position;
		originalPositionFace = faceAnimation.Position;
	}

	public override void _Process(float delta)
	{
		SearchDirection();
	}

	public void SearchDirection()
	{		
		if(canSearchDirection) {
		Vector2 targetPositionEyes = eyesAnimation.Position;
		Vector2 targetPositionFace = faceAnimation.Position;
		Vector2 targetPositionMouth = mouthAnimation.Position;

		//  ESQUERDA
		if (Input.IsActionJustPressed("Esquerda-cima"))
		{
			targetPositionEyes.x = originalPositionEyes.x - moveDistanceEyes;
			targetPositionEyes.y = originalPositionEyes.y - moveDistanceEyes;

			targetPositionFace.x = originalPositionFace.x - moveDistanceFace;
			targetPositionFace.y = originalPositionFace.y - moveDistanceFace;
		}
		else if (Input.IsActionJustPressed("Esquerda-meio"))
		{
			targetPositionEyes.x = originalPositionEyes.x - moveDistanceEyes;
			targetPositionEyes.y = originalPositionEyes.y;

			targetPositionFace.x = originalPositionFace.x - moveDistanceFace;
			targetPositionFace.y = originalPositionFace.y;
		}
		else if (Input.IsActionJustPressed("Esquerda-baixo"))
		{
			targetPositionEyes.x = originalPositionEyes.x - moveDistanceEyes;
			targetPositionEyes.y = originalPositionEyes.y + moveDistanceEyes;

			targetPositionFace.x = originalPositionFace.x - moveDistanceFace;
			targetPositionFace.y = originalPositionFace.y + moveDistanceFace;
		}
		//  MEIO
		else if (Input.IsActionJustPressed("Meio-cima"))
		{
			targetPositionEyes.x = originalPositionEyes.x;
			targetPositionEyes.y = originalPositionEyes.y - moveDistanceEyes;

			targetPositionFace.x = originalPositionFace.x;
			targetPositionFace.y = originalPositionFace.y - moveDistanceFace;
		}
		else if (Input.IsActionJustPressed("Meio-meio"))
		{
			targetPositionEyes.x = originalPositionEyes.x;
			targetPositionEyes.y = originalPositionEyes.y;

			targetPositionFace.x = originalPositionFace.x;
			targetPositionFace.y = originalPositionFace.y;
		}
		else if (Input.IsActionJustPressed("Meio-baixo"))
		{
			targetPositionEyes.x = originalPositionEyes.x;
			targetPositionEyes.y = originalPositionEyes.y + moveDistanceEyes;

			targetPositionFace.x = originalPositionFace.x;
			targetPositionFace.y = originalPositionFace.y + moveDistanceFace;
		}
		//  DIREITA
		else if (Input.IsActionJustPressed("Direita-cima"))
		{
			targetPositionEyes.x = originalPositionEyes.x + moveDistanceEyes;
			targetPositionEyes.y = originalPositionEyes.y - moveDistanceEyes;

			targetPositionFace.x = originalPositionFace.x + moveDistanceFace;
			targetPositionFace.y = originalPositionFace.y - moveDistanceFace;
		}
		else if (Input.IsActionJustPressed("Direita-meio"))
		{
			targetPositionEyes.x = originalPositionEyes.x + moveDistanceEyes;
			targetPositionEyes.y = originalPositionEyes.y;

			targetPositionFace.x = originalPositionFace.x + moveDistanceFace;
			targetPositionFace.y = originalPositionFace.y;
		}
		else if (Input.IsActionJustPressed("Direita-baixo"))
		{
			targetPositionEyes.x = originalPositionEyes.x + moveDistanceEyes;
			targetPositionEyes.y = originalPositionEyes.y + moveDistanceEyes;

			targetPositionFace.x = originalPositionFace.x + moveDistanceFace;
			targetPositionFace.y = originalPositionFace.y + moveDistanceFace;
		}

		if (targetPositionEyes != eyesAnimation.Position && targetPositionFace != faceAnimation.Position)
		{
			targetPositionMouth.x = targetPositionFace.x;
			targetPositionMouth.y = targetPositionFace.y;

			canSearchDirection = false;

			directionCooldown.Start();
			
			animationsLib.MoveToDirection(eyesAnimation, targetPositionEyes, ref tween, moveSpeedEyes);
			animationsLib.MoveToDirection(faceAnimation, targetPositionFace, ref tween, moveSpeedFace);
			animationsLib.MoveToDirection(mouthAnimation, targetPositionMouth, ref tween, moveSpeedMouth);
		}
		}
	}
	private void DirectionCooldown_timeout()
	{
		canSearchDirection = true;
	}
}
