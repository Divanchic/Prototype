using Godot;
using System;

public partial class Dialog : Node2D
{
	[Export] public Node2D Big_Dialog;
	[Export] public Node2D dialog; 
	[Export] public Label sing; 
	public void ButtonPressed(){
		dialog.Show();
		this.Hide();
	}
	public void End(){
		Big_Dialog.Hide();
		this.Hide();
		sing.Show();
		
	}
}
