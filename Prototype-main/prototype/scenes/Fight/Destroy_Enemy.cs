using Godot;
using System;

public partial class Destroy_Enemy : Area2D
{
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	public void OnBodyEntered(Node2D body){
		body.Free();
	}
}
