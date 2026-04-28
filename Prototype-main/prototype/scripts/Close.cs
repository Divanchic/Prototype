using Godot;
using System;

public partial class Close : Button
{
	[Export] public Sprite2D fone;
	
	public void ButtonPressed(){
		fone.Hide();
	}
}
