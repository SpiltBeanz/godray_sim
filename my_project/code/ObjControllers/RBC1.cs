using Sandbox;

// This code is shite, you know it I know it //
// Move on and call me and idiot later //

public sealed class RBC1 : Component
{
	[Property] public float speed { get; set; }
	[Property] private Rigidbody rb { get; set; }
	[Property] public float TimeMoving { get; set; }

	protected override void OnFixedUpdate()
	{
		if (Input.Pressed( "Use" ) && rb.Velocity.IsNearlyZero())
		{
			StartMoveForward(); // Object starts moving when player presses "E" if the object is not moving already //
		}
	}
	public void StartMoveForward() // Rigidbody moves forward at speed //
	{
		rb.Velocity = Vector3.Forward  * speed;
		Log.Info( rb.Velocity);
		ChangeDirection();// changes direction after a set period of time in seconds //	 
	}
	public async void ChangeDirection()
	{
		await Task.DelaySeconds(TimeMoving);
		rb.Velocity = Vector3.Right* speed; //Move right
		Log.Info( rb.Velocity);

		await Task.DelaySeconds(TimeMoving/4);
		rb.Velocity = Vector3.Backward* speed;// Move backward
		Log.Info( rb.Velocity);

		await Task.DelaySeconds(TimeMoving);
		rb.Velocity = Vector3.Left* speed;// Move left
		Log.Info( rb.Velocity);

		await Task.DelaySeconds(TimeMoving/4);
		StartMoveForward();// Repeat
	}
}
