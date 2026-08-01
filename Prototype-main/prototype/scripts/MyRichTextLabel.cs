using Godot;

public partial class MyRichTextLabel : RichTextLabel
{
	[Export]
	public float InjusticeSpeed { get; set; } = 0.05f;

	private Tween _currentTween;

	public override void _Ready()
	{
		if (Visible)
		{
			AnimateText();
		}
	}

	public override void _Notification(int what)
	{
		if (what == NotificationVisibilityChanged)
		{
			if (Visible)
			{
				AnimateText();
			}
			else
			{
				ResetTextAnimation();
			}
		}
	}

	private void AnimateText()
	{
		ResetTextAnimation();
		
		VisibleCharacters = 0;
		int totalChars = GetTotalCharacterCount();

		if (totalChars <= 0) return;

		_currentTween = CreateTween();
		_currentTween.TweenMethod(Callable.From<int>(UpdateVisibleCharacters), 0, totalChars, totalChars * InjusticeSpeed)
			.SetTrans(Tween.TransitionType.Linear)
			.SetEase(Tween.EaseType.InOut);
	}

	private void ResetTextAnimation()
	{
		if (_currentTween != null && _currentTween.IsValid())
		{
			_currentTween.Kill();
		}
		VisibleCharacters = 0;
	}

	private void UpdateVisibleCharacters(int value)
	{
		VisibleCharacters = value;
	}
}
