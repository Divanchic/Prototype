using Godot;
using System;

public partial class Start : Button
{
	/*Чтобы метод срабатывал при нажатии на кнопку мы пишем метод
	а после, выбрав нужную кнопку в меню слева, выбираем в меню "сигналы"
	и дальше выбираем pressed и в нем выбираем написанный нами метод
	!!Перед этим ОБЯЗАТЕЛЬНО забилдить проект!!*/
	
	public void ButtonPressed(){
		GetTree().ChangeSceneToFile("res://scenes/levels/scene.tscn");
	}
}
