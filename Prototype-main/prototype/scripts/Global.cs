using Godot;
using System;

public partial class Global : Node
{
	public int scene_index=0;
	public string[] scenes = [
		"res://scenes/levels/scene.tscn",
	];
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
