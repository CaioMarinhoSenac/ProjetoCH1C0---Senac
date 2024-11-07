using Godot;
using System;
using System.Linq.Expressions;

public partial class BaseState : AnimatedSprite2D
{
	protected AnimationsLib animationsLib = new AnimationsLib();

	protected StateManager StateManager;

	protected Color stateColor;

	protected AudioManager audioManager;

	protected Timer audioLoopTimer;

	protected float loopCooldown = 0f;

	protected AudioStreamPlayer2D audioSample;

	protected bool loopColor = false;

	private bool isAudioConnected = false;

	public virtual void EnterState(StateManager State)
	{
		StateManager = State;

		audioManager = StateManager.audioManager;

		SwitchStateAnimation();

		SwitchCircuitColor(stateColor);

		if (this.GetType().Name != "BobeiraState" && this.GetType().Name != "InstagramavelState")
		{
			audioSample = SearchAudioSample(this.GetType().Name);

			animationsLib.PlaySFX(audioSample);
		}
	}
	public virtual void LeaveState(StateManager State)
	{
		if (audioSample != null) 
        {
            audioSample.Stop();
        }
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

	protected AudioStreamPlayer2D SearchAudioSample(string audioName)
    {
        AudioStreamPlayer2D audioRef = audioManager.GetType().GetField(audioName)?.GetValue(audioManager) as AudioStreamPlayer2D;

        if (audioRef != null) return audioRef;

        else return null;
    }
}