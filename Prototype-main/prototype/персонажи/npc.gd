# Скрипт на Area2D (корневой узел)
extends Area2D

func _ready():
	# Подключаем сигналы
	body_entered.connect(_on_body_entered)
	area_entered.connect(_on_area_entered)

func _on_body_entered(body):
	print("Коснулся: ", body.name)  # Проверка, работает ли коллизия
	# Тут пиши что должно произойти (урон, подобрать предмет и т.д.)

func _on_area_entered(area):
	print("Коснулась другая зона: ", area.name)
