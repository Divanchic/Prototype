using Godot;
using System;

public partial class FightProgress : ProgressBar
{
	[Export] public ProgressBar fightprogress;
	[Export] public Timer timer;
	private SetAttach enemy;
	public float len = 0;
	public float chtime = 1f;
	[Export]public float times = 1f;
	public override void _Ready()
	{
		
		var enemy = GetNode<SetAttach>("/root/Scene/Enemy");
		for(int i=1; i<enemy.spawn.Length; i++){
			if((enemy.spawn[i]!=-1)&&(enemy.spawn[i]!=-2)){
				len+=0.5f + chtime;
			}
			if(enemy.spawn[i]==-1){
				chtime+=0.5f;
				
			}
			if(enemy.spawn[i]==-2){
				chtime-=0.5f;
			}
		} 
		timer.WaitTime = times;
		timer.Timeout += OnTimerTimeout;
		fightprogress.MaxValue = len;
	}
	
	private void OnTimerTimeout(){
		len-=1;
		fightprogress.Value = len;
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
}
