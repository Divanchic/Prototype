using Godot;
using System;

public partial class SetAttach : CharacterBody2D
{
	[Export] public float[] spawnTimes;
	[Export] public int[] spawn;
	[Export] public Node2D enemy_sprite;
}
