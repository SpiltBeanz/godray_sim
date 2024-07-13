using Sandbox;

public sealed class RBC2 : Component
{
	[Property] public float speed { get; set; }
	[Property] private Rigidbody rb { get; set; }
	[Property] public Vector3 rbVector { get; set; }
	[Property] public float TimeAlive { get; set; }

	protected override void OnFixedUpdate()
	{
		
		if (Input.Pressed( "Use" ))
		{
			if (rb.Velocity != 0 )  return;
			StartMove(); // Object starts moving when player presses "E" if the object is not moving already //
		}
				
	}
	public void StartMove() // Rigidbody moves in vector direction at set speed and deletes GameObject after period of time //
	{
		rb.Velocity = rbVector  * speed ;
		Log.Info( rb?.Velocity);
		Remove();	
	}

	public async void Remove()
	{
		await Task.DelaySeconds(TimeAlive);
		Transform.Position = Vector3.Up *-1000;
		await Task.DelaySeconds(.1f);
		GameObject.Destroy();
	}
}
