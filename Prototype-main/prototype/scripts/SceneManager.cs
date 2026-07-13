using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class SceneManager : Node2D
{
	public static SceneManager Instance { get; private set; }
	[Export] public NodePath ContainerPath;
	
	[Export] public ColorRect FadeRect; 
	[Export] public float FadeDuration = 0.4f;

	private Node _container;
	private Dictionary<string, Node> _sceneCache = new Dictionary<string, Node>();
	
	// Флаг для отслеживания самого первого запуска игры
	private bool _isFirstLoad = true;

	public override void _Ready()
	{
		Instance = this;
		_container = GetNode(ContainerPath);
		
		if (FadeRect == null)
		{
			FadeRect = GetNodeOrNull<ColorRect>("FadeLayer/FadeRect");
		}

		// Запускаем первую локацию
		ChangeLocation("res://scenes/levels/level.tscn", new Vector2(1730, -55)); 
	}

	public async void ChangeLocation(string scenePath, Vector2 spawnPosition)
	{
		var player = GetTree().GetFirstNodeInGroup("Player") as Movement;
		
		// Если это НЕ первый запуск — плавно уходим в черный экран
		if (!_isFirstLoad)
		{
			await Fade(1.0f);
		}
		else
		{
			// При первом запуске экран И ТАК черный (мы настроили это в инспекторе).
			// Гарантируем, что альфа точно равна 1, на случай правок в редакторе.
			if (FadeRect != null)
			{
				Color c = FadeRect.Modulate;
				c.A = 1.0f;
				FadeRect.Modulate = c;
			}
		}

		// Логика смены сцены (выполняется за черным экраном)
		if (_container.GetChildCount() > 0)
		{
			var currentScene = _container.GetChild(0);
			_container.RemoveChild(currentScene);
			currentScene.ProcessMode = ProcessModeEnum.WhenPaused; 
		}

		Node nextScene;

		if (_sceneCache.ContainsKey(scenePath))
		{
			nextScene = _sceneCache[scenePath];
			nextScene.ProcessMode = ProcessModeEnum.Inherit;
		}
		else
		{
			var sceneResource = GD.Load<PackedScene>(scenePath);
			nextScene = sceneResource.Instantiate();
			_sceneCache[scenePath] = nextScene;
		}

		_container.AddChild(nextScene);
		
		if (player != null)
		{
			player.GlobalPosition = spawnPosition;
		}

		DisableAllDoorsInScene(nextScene);

		// Сбрасываем флаг: все последующие вызовы будут использовать плавное затемнение
		_isFirstLoad = false;

		// Плавно открываем экран (из черного в прозрачный)
		await Fade(0.0f);
	}

	private async Task Fade(float targetAlpha)
	{
		if (FadeRect == null) return;

		Tween tween = CreateTween();
		Color targetColor = FadeRect.Modulate;
		targetColor.A = targetAlpha;
		
		tween.TweenProperty(FadeRect, "modulate", targetColor, FadeDuration)
			 .SetTrans(Tween.TransitionType.Quad)
			 .SetEase(Tween.EaseType.InOut);

		await ToSignal(tween, Tween.SignalName.Finished);
	}

	private async void DisableAllDoorsInScene(Node scene)
	{
		var doors = scene.FindChildren("*", "Area2D");
		
		foreach (Node node in doors)
		{
			if (node is Area2D door)
			{
				door.SetDeferred(Area2D.PropertyName.Monitoring, false);
			}
		}

		await ToSignal(GetTree().CreateTimer(FadeDuration), SceneTreeTimer.SignalName.Timeout);

		foreach (Node node in doors)
		{
			if (node is Area2D door)
			{
				door.SetDeferred(Area2D.PropertyName.Monitoring, true);
			}
		}
	}
}
