using Godot;
using System;

public partial class bush : Area2D
{
	private Movement player;
	[Export] public Node2D berry_bush;
	public int check;
	public override void _Ready()
	{
		
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public void OnBodyEntered(Node2D body){
		if(body.IsInGroup("Player")){
			check = 1;
		}
	}
	public void OnBodyExited(Node2D body){
		if(body.IsInGroup("Player")){
			check = 0;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		if(check == 1 ){
			if(Input.IsActionJustPressed("E")){
				var player = GetNode<Movement>("/root/Scene/Player");
				player.Berry += 1;
				berry_bush.Free();
				this.Free();
				check = 0;
			}
		}
	}
}
