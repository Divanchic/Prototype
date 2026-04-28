using Godot;
using System;

public partial class NPC : Area2D
{
	[Export] public Label sing;
	[Export] public Node2D Dialog;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}
	
	public void OnBodyEntered(Node2D body){
		if(body.IsInGroup("Player")){	
			sing.Show();
		}	
	}
	public void OnBodyExited(Node2D body){
		if(body.IsInGroup("Player")){	
			sing.Hide();
			Dialog.Hide();
		}	
	}
	
	public override void _Process(double delta)
	{
		if((sing.Visible==true)&&(Input.IsActionJustPressed("E"))&&(Dialog.Visible==false)){
			Dialog.Show();
			sing.Hide();
		}
	}
}
