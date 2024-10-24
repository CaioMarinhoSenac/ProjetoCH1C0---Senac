using Godot;
using System;

public partial class BaseState : AnimatedSprite
{
	protected AnimationsLib animationsLib = new AnimationsLib();

	protected Tween tween;

	protected StateManager StateManager;

	public virtual void EnterState(StateManager State)
	{
		StateManager = State;

		SwitchStateAnimation();
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
}
