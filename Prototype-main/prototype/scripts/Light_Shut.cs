using Godot;
using System;

public partial class Light_Shut : PointLight2D
{
	[Export] public Timer timer;
	[Export] public float times = 1f;
	public override void _Ready()
	{
		timer.WaitTime = times;
		timer.Timeout += OnTimerTimeout;
	}

	private void OnTimerTimeout(){
		if(this.Energy!=0.1){
			this.Energy -= 0.01f;
		}
		
	}
}
