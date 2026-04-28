using Godot;
using System;

public partial class destroy_hp : Area2D
{
	[Export] public Node2D Spawn;
	[Export] public Node2D Menu;
	[Export] public ProgressBar HelthBar;
	[Export] public int hp=10;
	public int check=0;
	public override void _Ready()
	{
		
		BodyEntered += OnBodyEntered;
	}
	
	/* Тут если стрелочка попала в область, то она удоляется и хп-1*/
	public void OnBodyEntered(Node2D body){
		if(check==0){
			HelthBar.Value -= 1;
			body.Free();
			hp-=1;
			GD.Print(hp);
			if(hp<1){
				Spawn.Free();
				Menu.Show();
			}
		}
		
	}
}
