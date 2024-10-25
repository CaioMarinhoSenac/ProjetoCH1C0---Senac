using Godot;
using System;

public partial class CanvasScript : CanvasLayer
{
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_cancel")){
			GetTree().Quit();
		}
	}
}
