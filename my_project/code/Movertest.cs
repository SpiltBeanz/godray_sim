using Sandbox;

// This code is shite, you know it I know it //
// Move on and call me and idiot later //

public sealed class Movertest : Component
{
	[Property] private GameObject movingblock { get; set; }
	[Property] public float TimetillMove { get; set; }
	[Property] public float TimetillChange { get; set; }
	public bool Reverse = false;
	public bool timerstarted = false;
	protected override void OnStart()
	{
		Move(); 
	}
	public void Move() 
	{
		var direction = Vector3.Left;
		if (Reverse) direction = Vector3.Right;
		{
			movingblock.Transform.Position += direction  * 52;
			MoveAgain();// Moves again after period of time in seconds //
		}
		if (!timerstarted)
		StartReverseTimer();	 
	}
	public async void MoveAgain()
	{
		await Task.DelaySeconds(TimetillMove);
		Move();// Repeat
	}
	public async void StartReverseTimer()
	{
		timerstarted = true;
		await Task.DelaySeconds(TimetillChange);
		Reverse = !Reverse;
		timerstarted = false;
	}
}
