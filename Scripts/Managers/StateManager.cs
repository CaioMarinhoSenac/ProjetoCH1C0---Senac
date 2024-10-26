using Godot;
using System;

public partial class StateManager : AnimatedSprite2D
{
	public AnimatedSprite2D faceAnimation;
	[Export] public VariantManager eyesAnimation;
	[Export] public AnimatedSprite2D mouthAnimation;
	public BaseState currentState;

	// INSTACIA AS CLASSES (ESTADOS)
	public TristeState tristeState = new TristeState();
	public MedoState medoState = new MedoState();
	public RaivaState raivaState = new RaivaState();
	public CoracaoState coracaoState = new CoracaoState();
	public FelizState felizState = new FelizState();

	public DormindoState dormindoState = new DormindoState();

	Vector2 originalPosition;

	bool canSwitchState = true;

	[Export] Timer stateCooldown;

	[Export] public AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		faceAnimation = this;

		originalPosition = faceAnimation.Position;

		stateCooldown.Timeout += StateCooldown_timeout;

		currentState = felizState;
		currentState.EnterState(this);
	}

	public override void _Process(double delta)
	{
		SearchState();
	}

	public void SearchState(){
		BaseState newState = currentState;

		if(Input.IsActionJustPressed("RaivaState")){
			newState = raivaState;
		}
		else if(Input.IsActionJustPressed("TristeState")){
			newState = tristeState;
		}
		else if(Input.IsActionJustPressed("FelizState")){
			newState = felizState;
		}
		else if(Input.IsActionJustPressed("MedoState")){
			newState = medoState;
		}
		else if(Input.IsActionJustPressed("CoracaoState")){
			newState = coracaoState;
		}
		else if(Input.IsActionJustPressed("DormindoState")){
			newState = dormindoState;
		}

		if(newState != currentState)
		{
			SwitchState(newState);
		}
	}   

	public void SwitchState(BaseState state)
	{
		if(canSwitchState){

		canSwitchState = false;
			
		stateCooldown.Start();

		// sai do estado atual
		currentState.LeaveState(this);

		// muda o estado atual
		currentState = state;

		// entra no novo estado
		currentState.EnterState(this);
		}
	}

	private void StateCooldown_timeout()
	{
		canSwitchState = true;
	}
}
