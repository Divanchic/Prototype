using Godot;
using System;

public partial class SetAttach : CharacterBody2D
{
	[Export] public float[] spawnTimes;
	[Export] public int[] spawn;
	[Export] public Node2D enemy_sprite;
	
	[Export] public float speed = 200.0f;
	public int Id = 0;
	[Export] public Node2D[] points; 
	public override void _Process(double delta)
	{
		Node2D point = points[Id];
		GlobalPosition = GlobalPosition.MoveToward(point.GlobalPosition, speed*(float)delta);
		if(GlobalPosition==point.GlobalPosition){
			Id+=1;
			if(Id>=points.Length){
				Id=0;
			}
		}
	}
}
