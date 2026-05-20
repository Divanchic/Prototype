using Godot;
using System;
using System.Collections.Generic;

public partial class Spawn : Node2D 
{
	[Export] public AudioStreamPlayer2D audioPlayer; 
	[Export] public Node2D spawnpoint;
	[Export] public Node2D enemyplace;

	[Export] public float secondsEarlier = 1.0f; 

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
	public int check = 0;
	private bool isFinished = false;

	public override void _Ready()
	{
		player = GetNode<Movement>("/root/Scene/Player");
		enemy = GetNode<SetAttach>("/root/Scene/Enemy");

		if (enemy == null)
		{
			GD.PrintErr("[SPAWN ERROR]: Враг (SetAttach) не найден по пути /root/Scene/Enemy!");
			return;
		}

		spawns = enemy.spawn;
		spawnTimes = enemy.spawnTimes; 

		// ПРОВЕРКА 1: Заполнены ли массивы у врага?
		if (spawns == null || spawnTimes == null || spawns.Length == 0 || spawnTimes.Length == 0)
		{
			GD.PrintErr($"[SPAWN ERROR]: Массивы пустые! Spawns length: {spawns?.Length}, SpawnTimes length: {spawnTimes?.Length}");
			return;
		}

		// ПРОВЕРКА 2: Совпадают ли размеры массивов?
		if (spawns.Length != spawnTimes.Length)
		{
			GD.PrintErr($"[SPAWN ERROR]: Длина массива spawns ({spawns.Length}) не совпадает с spawnTimes ({spawnTimes.Length})!");
		}
		
		enemy.enemy_sprite.Position = enemyplace.Position;

		foreach (var path in arrows)
		{
			cachedArrows.Add(GD.Load<PackedScene>(path));
		}

		// Запуск музыки с принудительным сбросом позиции
		if (audioPlayer != null)
		{
			audioPlayer.Stop(); // Сбрасываем, если была включена автоигра
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

		// Если музыка почему-то не играет, стрелы не полетят. Проверяем это.
		if (!audioPlayer.Playing && check < spawns.Length)
		{
			return; 
		}

		float playbackTime = (float)audioPlayer.GetPlaybackPosition();
		playbackTime += (float)AudioServer.GetTimeSinceLastMix() - (float)AudioServer.GetOutputLatency();

		// Безопасная проверка: если (время - secondsEarlier) уходит в минус,
		// стрела должна вылететь сразу же на старте (при playbackTime >= 0)
		while (check < spawns.Length)
		{
			float targetTime = spawnTimes[check] - secondsEarlier;
			
			// Если расчетное время пришло ИЛИ если оно отрицательное, а игра уже началась
			if (playbackTime >= targetTime)
			{
				GD.Print($"[SPAWN]: Спавню стрелу {check} (тип: {spawns[check]}) на времени трека {playbackTime} сек. (Цель была: {spawnTimes[check]} сек.)");
				SpawnArrow(spawns[check]);
				check++;
			}
			else
			{
				break; // Время для следующей стрелы еще не подошло
			}
		}

		if (check >= spawns.Length && !isFinished)
		{
			EndSong();
		}
	}

	private void SpawnArrow(int arrowIndex)
	{
		if (arrowIndex < 0 || arrowIndex >= cachedArrows.Count) return;

		Node2D arrow = (Node2D)cachedArrows[arrowIndex].Instantiate();
		arrow.Position = spawnpoint.Position;
		AddChild(arrow);
	}

	private void EndSong()
	{
		isFinished = true;
		GD.Print("[SPAWN INFO]: Песня завершена, очистка сцены.");
		player.Speed = 300f;
		player.Show();
		enemy.Free();
		this.Free();
	}
}
