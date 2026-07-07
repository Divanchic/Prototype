using Godot;
using System;

public partial class Door : Area2D
{
	[Export] public string TargetScenePath;
	[Export] public Vector2 SpawnPosition;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			Teleport();
		}
	}

	private async void Teleport()
	{
		if (string.IsNullOrEmpty(TargetScenePath)) return;
		SetDeferred(PropertyName.Monitoring, false);
		SceneManager.Instance.ChangeLocation(TargetScenePath, SpawnPosition);
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		SetDeferred(PropertyName.Monitoring, true);
	}
}
