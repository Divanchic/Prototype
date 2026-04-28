using Godot;
using System;

public partial class arrow_area : Area2D
{ /* Попытка сделать атаку, если объект с тегом arrow входит в область
	то мы записываем его в check, и если до момента выхода из области
	мы нажмем нужную стрелочку то объект исчезнет*/
	[Export] public Node2D sprite_for_long;
	[Export] public destroy_hp destroy;
	public Node2D check;
	public string[] buttons = ["ui_left", "ui_right", "ui_up", "ui_down"];
	public override void _Ready()
	{
	/* Добавляем функции входа объекта и его выхода*/
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}
	public void OnBodyEntered(Node2D body){
		if(body.IsInGroup("arrow")){	
			check = body;
		}	
	}
	public void OnBodyExited(Node2D body){
		if(body.IsInGroup("arrow")){
			if(check.IsInGroup("ui_long_left")){
				check.Free();
				destroy.Show();
				destroy.check = 0;
				sprite_for_long.Hide();
			}
			check = null;
		}	
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (check == null) return;
		if (check.IsInGroup("ui_long_left"))
		{
			// Проверяем зажата ли клавиша 'A' (или привязанное действие)
			if (Input.IsKeyPressed(Key.A)) {
				destroy.check = 1;
				sprite_for_long.Show();
			}
		}
		/* Перебираем все элементы массива кнопок, и если мы нажимем кнопку, 
		которая совпадает с вошедшей стрелочкой то мы ее удаляем*/
		foreach(string but in buttons){
			if(Input.IsActionJustPressed(but)){
				if(check.IsInGroup(but)){
					check.Free();
				}
			}
		}
		
	}
	
}
