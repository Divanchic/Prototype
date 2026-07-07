using Godot;
using System;
using System.Collections.Generic;

public partial class Spawn : Node2D
{
	[Export] public AudioStreamPlayer2D audioPlayer; 
	[Export] public Node2D spawnpoint;
	[Export] public Node2D enemyplace;
	[Export] public Node2D enemySpawnpoint;
	
	[Export] public float secondsEarlier = 1.0f; 
	[Export] public float enemySecondsEarlier = 1.5f;

	private Movement player;
	private SetAttach enemy;

	public string[] arrows = [
		"res://scenes/Attack/a.tscn",
		"res://scenes/Attack/d.tscn",
		"res://scenes/Attack/s.tscn",
		"res://scenes/Attack/w.tscn",
		"res://scenes/Attack/void.tscn",
		"res://scenes/Attack/long_a.tscn"
	];

	private List<PackedScene> cachedArrows = new List<PackedScene>();
	public int[] spawns;
	public float[] spawnTimes;
	
	public int playerCheck = 0;
	public int enemyCheck = 0;
	private bool isFinished = false;

	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("Player") as Movement;
		enemy = GetTree().GetFirstNodeInGroup("enemy") as SetAttach;

		if (enemy == null)
		{
			GD.PrintErr("[SPAWN ERROR]: Враг (SetAttach) не найден по пути /root/Scene/CurrentScene/Enemy!");
			return;
		}

		spawns = enemy.spawn;
		spawnTimes = enemy.spawnTimes; 

		if (spawns == null || spawnTimes == null || spawns.Length == 0 || spawnTimes.Length == 0)
		{
			GD.PrintErr($"[SPAWN ERROR]: Массивы пустые! Spawns length: {spawns?.Length}, SpawnTimes length: {spawnTimes?.Length}");
			return;
		}

		if (spawns.Length != spawnTimes.Length)
		{
			GD.PrintErr($"[SPAWN ERROR]: Длина массива spawns ({spawns.Length}) не совпадает с spawnTimes ({spawnTimes.Length})!");
		}
		
		enemy.enemy_sprite.Position = enemyplace.Position;

		foreach (var path in arrows)
		{
			cachedArrows.Add(GD.Load<PackedScene>(path));
		}

		if (audioPlayer != null)
		{
			audioPlayer.Stop(); 
			audioPlayer.Play();
			GD.Print("[SPAWN INFO]: Музыка успешно запущена.");
		}
		else
		{
			GD.PrintErr("[SPAWN ERROR]: AudioStreamPlayer не привязан в инспекторе!");
		}
	}

	public override void _Process(double delta)
	{
		if (isFinished || audioPlayer == null || spawns == null || spawnTimes == null) return;

		if (!audioPlayer.Playing && playerCheck < spawns.Length)
		{
			return; 
		}

		float playbackTime = (float)audioPlayer.GetPlaybackPosition();
		playbackTime += (float)AudioServer.GetTimeSinceLastMix() - (float)AudioServer.GetOutputLatency();

		while (enemyCheck < spawns.Length)
		{
			float enemyTargetTime = spawnTimes[enemyCheck] - enemySecondsEarlier;
			
			if (playbackTime >= enemyTargetTime)
			{
				SpawnArrow(spawns[enemyCheck], true);
				enemyCheck++;
			}
			else
			{
				break; 
			}
		}

		while (playerCheck < spawns.Length)
		{
			float playerTargetTime = spawnTimes[playerCheck] - secondsEarlier;
			
			if (playbackTime >= playerTargetTime)
			{
				GD.Print($"[SPAWN PLAYER]: Стрела {playerCheck} на времени {playbackTime} сек.");
				SpawnArrow(spawns[playerCheck], false); 
				playerCheck++;
			}
			else
			{
				break; 
			}
		}

		if (playerCheck >= spawns.Length && enemyCheck >= spawns.Length && !isFinished)
		{
			EndSong();
		}
	}

	private void SpawnArrow(int arrowIndex, bool isEnemyArrow)
	{
		if (arrowIndex < 0 || arrowIndex >= cachedArrows.Count) return;

		Node2D arrow = (Node2D)cachedArrows[arrowIndex].Instantiate();
		arrow.Scale *= 2;

		if (isEnemyArrow)
		{
			arrow.Position = Vector2.Zero;
			enemySpawnpoint.AddChild(arrow);
		}
		else
		{
			arrow.Position = spawnpoint.Position;
			AddChild(arrow);
		}

		
	}

	private void EndSong()
	{
		isFinished = true;
		GD.Print("[SPAWN INFO]: Песня завершена, очистка сцены.");
		player.Speed = 50f;
		player.Show();
		enemy.Free();
		this.Free();
	}
}
