using Godot;

public partial class OnShadows : Area2D
{
	[Export] public double DelaySeconds = 0.5;
	[Export] public Node2D TP;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private async void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player" || body is CharacterBody2D)
		{
			BodyEntered -= OnBodyEntered;

			await ToSignal(GetTree().CreateTimer(DelaySeconds), SceneTreeTimer.SignalName.Timeout);

			if (GodotObject.IsInstanceValid(body))
			{
				var lightAndShadows = body.GetNodeOrNull<CanvasItem>("LightAndShadows");
				if (lightAndShadows != null)
				{
					lightAndShadows.Visible = true;
				}
			}

			QueueFree();
			TP.QueueFree();
		}
	}
	
}
