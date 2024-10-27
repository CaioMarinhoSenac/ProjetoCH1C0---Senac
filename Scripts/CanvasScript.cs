using Godot;
using System;

public partial class CanvasScript : CanvasLayer
{
	[Export] public CanvasModulate canvasModulate;
	
	[Export] public Gradient gradient;

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_cancel")){
			GetTree().Quit();
		}

		SearchDayTime();
	}

	private void SearchDayTime(){
		var systemTime = Time.GetTimeDictFromSystem();

		float timeValue = Convert.ToSingle($"{systemTime["hour"]:00}{systemTime["minute"]:00}");

		float timePercent = timeValue / 2400.0f;

		canvasModulate.Color = gradient.Sample(timePercent);
	}
}
