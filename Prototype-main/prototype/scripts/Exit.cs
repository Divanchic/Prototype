using Godot;
using System;

public partial class Exit : Button
{
	public void ButtonPressed(){
		GetTree().ChangeSceneToFile("res://scenes/levels/Menu.tscn");
	}
}
