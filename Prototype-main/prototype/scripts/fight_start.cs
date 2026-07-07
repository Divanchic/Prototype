using Godot;
using System;

public partial class fight_start : Area2D
{
	private Movement player;
	[Export] public int big_zone;
	[Export] public string fight_in_files;
	PackedScene packed;

	private bool isFightStarted = false;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	public void OnBodyEntered(Node2D body)
	{
		if (isFightStarted) return;

		if (body.IsInGroup("Player"))
		{
			player = GetTree().GetFirstNodeInGroup("Player") as Movement;

			if (player == null) return;

			GD.Print(player.hide);

			if (big_zone == 1)
			{
				if (player.hide == 0)
				{
					StartFight(body);
				}
			}
			else if (big_zone == 0)
			{
				StartFight(body);
			}
		}
	}

	
	private void StartFight(Node2D body)
	{
		isFightStarted = true;

		SetDeferred(PropertyName.Monitoring, false);

		GD.Print("Check / Запуск боя");
		player.Hide();
		player.Speed = 0f;

		packed = (PackedScene)GD.Load(fight_in_files);
		Node2D fight = (Node2D)packed.Instantiate();
		fight.Position = body.Position;
		
		this.AddChild(fight);
	}
}
