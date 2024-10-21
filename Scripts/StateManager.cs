using Godot;
using System;

public partial class StateManager : AnimatedSprite
{
    public AnimatedSprite faceAnimation;
    public VariantManager eyesAnimation;
	public BaseState currentState;

	// INSTACIA AS CLASSES (ESTADOS)
	public TristeState tristeState = new TristeState();
	public MedoState medoState = new MedoState();
	public RaivaState raivaState = new RaivaState();
	public CoracaoState coracaoState = new CoracaoState();
	public FelizState felizState = new FelizState();

    Vector2 originalPosition;

	public override void _Ready()
	{
        faceAnimation = this;
        eyesAnimation = GetNode<VariantManager>("../OlhosSprite2D");

        originalPosition = faceAnimation.Position;

		currentState = felizState;
		currentState.EnterState(this, originalPosition);
	}

	public override void _Process(float delta)
	{
        SearchState();
        currentState.SearchDirection(faceAnimation.Position);
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

        if(newState != currentState)
        {
            SwitchState(newState);
        }
    }   

	public void SwitchState(BaseState state)
	{
		// sai do estado atual
		currentState.LeaveState(this);

		// muda o estado atual
		currentState = state;

		// entra no novo estado
		currentState.EnterState(this, originalPosition);
	}
}
