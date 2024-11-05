using Godot;
using System;

public partial class BaseState : AnimatedSprite2D
{
	protected AnimationsLib animationsLib;

	protected StateManager StateManager;

	protected Color stateColor;

	protected bool loopColor = false;

	public virtual void EnterState(StateManager State, VariantManager Variant)
	{
		StateManager = State;

		animationsLib = State.animationsLib;

		SwitchStateAnimation();

		SwitchCircuitColor(stateColor);
	}
	public virtual void LeaveState(StateManager State)
	{
	}

	public virtual void SwitchStateAnimation()
	{
		if (this.GetType().Name != null)
		{
			string animationName = this.GetType().Name;

			animationsLib.PlayFrameAnimation(StateManager.faceAnimation, animationName);
			animationsLib.PlayFrameAnimation(StateManager.eyesAnimation, animationName);
			animationsLib.PlayFrameAnimation(StateManager.mouthAnimation, animationName);
		}
	}

	public virtual void SwitchCircuitColor(Color targetColor)
	{
		animationsLib.SwitchColor(StateManager.CircuitColor, stateColor, 1.0f, loopColor);
	}
}
