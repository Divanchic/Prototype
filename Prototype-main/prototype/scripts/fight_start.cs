using Godot;
using System;

public partial class fight_start : Area2D
{
	/* */
	private Movement player;
	[Export] public int big_zone;
	[Export] public string fight_in_files;
	PackedScene packed;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		
	}

	public void OnBodyEntered(Node2D body){
		if(body.IsInGroup("Player")){
			var player = GetNode<Movement>("/root/Scene/Player");
			GD.Print(player.hide);
			if(big_zone==1){
				if(player.hide==0){
					GD.Print("Check");
					player.Hide();
					packed = (PackedScene)GD.Load(fight_in_files);
					Node2D fight = (Node2D)packed.Instantiate();
					fight.Position = body.Position;
					this.AddChild(fight);
					player.Speed = 0f;
				}
			}
			if(big_zone==0){
				GD.Print(player.hide);
				player.Hide();
				packed = (PackedScene)GD.Load(fight_in_files);
				Node2D fight = (Node2D)packed.Instantiate();
				fight.Position = body.Position;
				this.AddChild(fight);
				player.Speed = 0f;
			}
			
		}
	}
}
