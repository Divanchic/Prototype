using Godot;
using System;

public partial class Player_Area : Area2D
{
	[Export] public Node2D check;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public void OnBodyEntered(Node2D body){
		if(body.IsInGroup("Enemy")){
			check.Hide();
		}
	}
	public void OnBodyExited(Node2D body){
		if(body.IsInGroup("Enemy")){
			check.Show();
		}
	}
}
