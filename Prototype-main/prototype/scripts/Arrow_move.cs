using Godot;

public partial class Arrow_move : CharacterBody2D
{
	[Export] public float Speed = -150.0f;
	private float currentSpeed;

	public override void _Ready()
	{
		currentSpeed = Speed;

		// 1. Берем родителя (узел "s")
		var myRoot = GetParentOrNull<Node2D>();
		if (myRoot != null)
		{
			// 2. Берем родителя нашего родителя (точку, куда спавнер добавил узел "s")
			var globalParent = myRoot.GetParentOrNull<Node2D>();
			
			// 3. Проверяем имя этой точки
			if (globalParent != null && globalParent.Name == "enemySpawnpoint")
			{
				// Разворачиваем скорость для вражеской стрелы
				currentSpeed = -1 * Speed;
			
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// Движение самого CharacterBody2D
		Velocity = new Vector2(currentSpeed, 0);
		MoveAndSlide();
	}
}
