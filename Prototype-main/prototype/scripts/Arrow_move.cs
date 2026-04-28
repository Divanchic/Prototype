using Godot;
using System;

public partial class Arrow_move : CharacterBody2D
{
	[Export]public float Speed = 300.0f;
	[Export]public Vector2 direction;
	/* Тут мы просто придаем стрелочкам направление и движение*/
		public override void _Process(double delta)
		{
			Velocity = direction * Speed;
			MoveAndSlide();
		}
}
