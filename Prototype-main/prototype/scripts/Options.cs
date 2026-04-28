using Godot;
using System;

public partial class Options : Button
{
	//Экспорт позволяет задавать переменную в редакторе//
	[Export] public Sprite2D fone;
	//Show открывает скрытый спрайт, это видно в пункте visible//
	public void ButtonPressed(){
		if(fone.Visible==false){
			fone.Show();
		}
		else{
			fone.Hide();
		}
	}
}
