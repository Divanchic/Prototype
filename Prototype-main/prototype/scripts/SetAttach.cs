using Godot;
using System;

public partial class SetAttach : CharacterBody2D
{
	[Export] public int[] spawn;
	[Export] public float time;
	[Export] public Node2D enemy_sprite;
}
