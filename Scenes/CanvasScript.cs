using Godot;
using System;

public class CanvasScript : CanvasLayer
{
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("ui_cancel")){
            GetTree().Quit();
        }
    }
}
