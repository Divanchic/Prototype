using Godot;
using System;

public partial class camera : Camera2D
{  
	/*Попытка сделать более интересную работу камеры
	ЯВНЫЙ ПРОТОТИП - ЧИТАТЬ КОД НА СВОЙ СТРАХ И РИСК*/
	[Export] public Node2D player;
	[Export] public float left=-800f;
	[Export] public float right=800f;
	[Export] public float top=-800f;
	[Export] public float bottom=800f;
	public override void _Process(double delta)
	{
		
		if(player.GlobalPosition.X < left){
			this.LimitLeft-=800;
			this.LimitRight-=800;
			left-=800f;
			right-=800f;
		}
		if(player.GlobalPosition.Y < top){
			this.LimitTop-=800;
			this.LimitBottom-=800;
			bottom-=800f;
			top-=800f;
		}
		if(player.GlobalPosition.X > right){
			this.LimitLeft+=800;
			this.LimitRight+=800;
			left+=800f;
			right+=800f;
		}
		if(player.GlobalPosition.Y > bottom){
			this.LimitTop+=800;
			this.LimitBottom+=800;
			top+=800f;
			bottom+=800f;
		}
	}
}
