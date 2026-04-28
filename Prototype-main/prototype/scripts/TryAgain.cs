using Godot;
using System;

public partial class TryAgain : Button
{
	private string _scLoad;

	public override void _Ready()
	{
		var glob = GetNode<Global>("/root/Global");
		_scLoad = glob.scenes[glob.scene_index];
	}
	public void ButtonPressed(){
		GetTree().ChangeSceneToFile(_scLoad);
	}
}
