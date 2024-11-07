using Godot;
using System;

public partial class StateManager : AnimatedSprite2D
{
	public AnimatedSprite2D faceAnimation;
	[Export] public VariantManager eyesAnimation;
	[Export] public AnimatedSprite2D mouthAnimation;
	[Export] public Timer tristeTimer;
	[Export] public Timer dormindoTimer;

	public BaseState currentState;

	// INSTACIA AS CLASSES (ESTADOS)
	public TristeState tristeState = new TristeState();
	public MedoState medoState = new MedoState();
	public RaivaState raivaState = new RaivaState();
	public CoracaoState coracaoState = new CoracaoState();
	public FelizState felizState = new FelizState();
	public DormindoState dormindoState = new DormindoState();
	public InstagramavelState instagramavelState = new InstagramavelState();
	public BobeiraState bobeiraState = new BobeiraState();
	public AnsiedadeState ansiedadeState = new AnsiedadeState();
	public PiscandoState piscandoState = new PiscandoState();
	public ThugLifeState thugLifeState = new ThugLifeState();

	Vector2 originalPosition;

	bool canSwitchState = true;

	[Export] Timer stateCooldown;

	[Export] public AnimationPlayer animationPlayer;

	[Export] public Sprite2D CircuitColor;

	[Export] public AudioManager audioManager;

	public override void _Ready()
	{
		faceAnimation = this;

		originalPosition = faceAnimation.Position;

		stateCooldown.Timeout += StateCooldown_timeout;

		currentState = felizState;

		tristeTimer.Start();
		dormindoTimer.Start();

		tristeTimer.Timeout += TristeTimer_timeout;
		dormindoTimer.Timeout += DormindoTimer_timeout;

		currentState.EnterState(this);
	}

	public override void _Process(double delta)
	{
		SearchState();
	}

	public void SearchState()
	{
		BaseState newState = currentState;

		if (Input.IsActionJustPressed("InstagramavelState"))
		{
			newState = instagramavelState;

			stateCooldown.WaitTime = 5f;
		}
		else if (Input.IsActionJustPressed("CoracaoState"))
		{
			newState = coracaoState;

			stateCooldown.WaitTime = 5f;
		}
		else
		{
			if (Input.IsActionJustPressed("FelizState"))
			{
				newState = felizState;
			}
			else if (Input.IsActionJustPressed("MedoState"))
			{
				newState = medoState;

				stateCooldown.WaitTime = 1.75f;
			}
			else if (Input.IsActionJustPressed("ThugLifeState"))
			{
				newState = thugLifeState;
			}
			else if (Input.IsActionJustPressed("PiscandoState"))
			{
				newState = piscandoState;
			}
			else if (Input.IsActionJustPressed("BobeiraState"))
			{
				newState = bobeiraState;
			}
			else if (Input.IsActionJustPressed("AnsiedadeState"))
			{
				newState = ansiedadeState;
			}
			else if (Input.IsActionJustPressed("RaivaState"))
			{
				newState = raivaState;
			}
		}

		if (newState != currentState)
		{
			if (currentState == dormindoState)
			{
				SwitchState(ansiedadeState);
			}
			else
			{
				SwitchState(newState);
			}

			ResetTimers();
		}
	}

	public void SwitchState(BaseState state)
	{
		if (canSwitchState)
		{
			// sai do estado atual
			currentState.LeaveState(this);

			canSwitchState = false;

			stateCooldown.Start();

			// muda o estado atual
			currentState = state;

			// entra no novo estado
			currentState.EnterState(this);
		}
	}

	private void StateCooldown_timeout()
	{
		canSwitchState = true;

		stateCooldown.WaitTime = 2.5f;
	}

	private void TristeTimer_timeout()
	{
		SwitchState(tristeState);
	}

	private void DormindoTimer_timeout()
	{
		SwitchState(dormindoState);
	}

	private void ResetTimers()
	{
		tristeTimer.Stop();
		dormindoTimer.Stop();

		tristeTimer.Start();
		dormindoTimer.Start();
	}
}
