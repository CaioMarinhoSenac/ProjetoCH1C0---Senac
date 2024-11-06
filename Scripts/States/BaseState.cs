using Godot;
using System;

public partial class BaseState : AnimatedSprite2D
{
	protected AnimationsLib animationsLib = new AnimationsLib();

	protected StateManager StateManager;

	protected Color stateColor;

	protected AudioManager audioManager;

	protected AudioStreamPlayer2D audioSample;

	protected bool loopColor = false;

	public virtual void EnterState(StateManager State)
	{
		StateManager = State;

		audioManager = StateManager.audioManager;

		SwitchStateAnimation();

		SwitchCircuitColor(stateColor);

		string audioName = GetType().Name;

		audioSample = PlaySFX(audioName);
	}
	public virtual void LeaveState(StateManager State)
	{
		animationsLib.FadeOutAudio(audioSample, 2f);
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

	protected AudioStreamPlayer2D PlaySFX(string audioName)
	{
		AudioStreamPlayer2D audioClip = audioManager.GetType().GetField(audioName)?.GetValue(audioManager) as AudioStreamPlayer2D;

		audioClip.VolumeDb = 0;

		audioClip.Play();

		return audioClip;
	}
}
