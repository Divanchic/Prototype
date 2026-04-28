using Godot;
using System;
using System.Collections.Generic;

public partial class SettingsMenu : Control
{
	[Export] private OptionButton _resButton;

	private readonly Dictionary<string, Vector2I> _resolutions = new()
	{
		{"1920x1080", new Vector2I(1920, 1080)}, 
		{"1280x720", new Vector2I(1280, 720)},
		{"1600x900", new Vector2I(1600, 900)},
		{"1920x1200", new Vector2I(1920, 1200)}
	};

	public override void _Ready()
	{
		_resButton.Clear();
		foreach (var res in _resolutions.Keys)
		{
			_resButton.AddItem(res);
		}
		
		_resButton.ItemSelected += OnResolutionSelected;
	}

	private void OnResolutionSelected(long index)
	{
		string key = _resButton.GetItemText((int)index);
		GetWindow().Size = _resolutions[key];
		
		Vector2I screenSize = DisplayServer.ScreenGetSize();
		GetWindow().Position = (screenSize - GetWindow().Size) / 2;
	}
	
	public void OnBorderlessToggled(bool isToggled)
	{
		GetWindow().Borderless = isToggled;
	}

	public void OnFullscreenToggled(bool isToggled)
	{
		if (isToggled)
		{
			GetWindow().Mode = Window.ModeEnum.ExclusiveFullscreen;
		}
		else
		{
			GetWindow().Mode = Window.ModeEnum.Windowed;
		}
	}
}
