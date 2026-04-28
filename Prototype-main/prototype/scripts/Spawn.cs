using Godot;
using System;

public partial class Spawn : Node2D
{
	[Export] public Timer timer;
	private Movement player;
	private SetAttach enemy;
	/* Это код для появления стрелочек, в массив spawns пишутся числа
	это стрелочки которые будут вылетать, например [0,1]-это влево вправо*/
	public int[] spawns;
	/* В массив arrows вписываем ссылки на стрелочки 
	которые хранятся в папке attack */
	public string[] arrows = ["res://scenes/Attack/a.tscn", 
	"res://scenes/Attack/d.tscn", 
	"res://scenes/Attack/s.tscn", 
	"res://scenes/Attack/w.tscn",
	"res://scenes/Attack/void.tscn",
	"res://scenes/Attack/long_a.tscn",
	];
	/* Создаем запакованную сцену */
	PackedScene packed;
	public int check=0;
	//[Export] public ProgressBar fightprogress;
	[Export] public Node2D spawnpoint;
	[Export] public Node2D enemyplace;
	/* при начале кода мы обращаемся к таймеру, когда он равен нулю
	то испольняется функция OnTimerTimeout*/
	public override void _Ready(){
		var enemy = GetNode<SetAttach>("/root/Scene/Enemy");
		spawns = enemy.spawn;
		timer.WaitTime = enemy.time;
		timer.Timeout += OnTimerTimeout;
		enemy.enemy_sprite.Position = enemyplace.Position;
		//fightprogress.MaxValue = spawns.Length;
	}
	/* проверяем чтобы проверочная переменная не выходила за рамки массива
	после мы выбираем нужную стрелочку, загружаем ее, а после стрелка 
	принимает нужную позицию*/
	private void OnTimerTimeout(){
		if((check<spawns.Length)){
			int ar = spawns[check];
			switch (ar){
				case -1:
					timer.WaitTime += 0.5f;
				
					break;
				case -2:
					timer.WaitTime -= 0.5f;
				
					break;
				default:
	   				packed = (PackedScene)GD.Load(arrows[ar]);
					Node2D arrow = (Node2D)packed.Instantiate();
					arrow.Position = spawnpoint.Position;
					this.AddChild(arrow);
					
					break;
			}
			check+=1;
			
		
			//fightprogress.Value = ((spawns.Length) - check);
			
			
		}
		else{
			var player = GetNode<Movement>("/root/Scene/Player");
			var enemy = GetNode<SetAttach>("/root/Scene/Enemy");
			player.Speed = 300f;
			player.Show();
			enemy.Free();
			this.Free();
		}
	}
}
