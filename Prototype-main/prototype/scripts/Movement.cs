using Godot;
using System;

public partial class Movement : CharacterBody2D
{
	
	//Экспорт позвоялет менять значение переменной в редакторе//
	public int Berry = 2;
	public float Speed = 300.0f;
	public int hide=0;
	[Export]public Node2D menu;
	[Export]public PointLight2D light;
	[Export]public Label Berrys;
	[Export]public CollisionShape2D Shape;
	public override void _Ready(){
	
	}
	public override void _PhysicsProcess(double delta)
	{
		Berrys.Text = Berry.ToString();
		if(Berry>0){
			if(Input.IsActionJustPressed("T")){
				light.Energy = 1;
				Berry-=1;
			}
		}
		/*Получаем направление передвижения в зависимости от нажатых клавиш
		заданных в Проект/Настройки Проекта/Список действий */
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = direction * Speed;
		
		MoveAndSlide();
		/*Ниже при нажатии на Esc и если menu скрыто то оно появляется
		и наоборот*/
		if(Input.IsActionJustPressed("ui_cancel")){
			if(menu.Visible==false){
				menu.Show();
				Speed = 0f;
			}
			else{
				menu.Hide();
				Speed = 300f;
			}
		}
		/*Ниже при нажатии на Пробел и если свет не горит то он загорается
		и наоборот*/
		if(Input.IsActionJustPressed("ui_select")){
			if(light.Energy==1){
				GD.Print(hide);
				light.Energy = 0.1f;
				hide = 1;
				Speed = 150f;
			}
			else{
				light.Energy = 1;
				hide = 0;
				Speed = 300f;
			}
		}
	}
}
