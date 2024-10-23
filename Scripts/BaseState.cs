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

		SwitchFace();
	}
	public virtual void LeaveState(StateManager State)
	{
	}

	public virtual void SwitchFace()
	{
		if (this.GetType().Name != null)
		{
			animationsLib.PlayFrameAnimation(StateManager.faceAnimation, this.GetType().Name);
		}
	}
}
