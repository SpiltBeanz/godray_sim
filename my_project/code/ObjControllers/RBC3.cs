using Sandbox;
public class RBC3 : Component

{
	[Property] public float speed { get; set; }
	[Property] private Rigidbody rb { get; set; }
	[Property] public float TimeMoving { get; set; }
	private TimeUntil timetochange;
    private bool alreadymoved;
	protected override void OnFixedUpdate()
	{
		
	    if (Input.Pressed( "Use" ) && rb.Velocity.IsNearlyZero() && !alreadymoved)
		{
			// Object starts moving when player presses "E" if the object is not moving already //
		    StartMoveForward(); 

            timetochange = TimeMoving;
            Log.Info(timetochange);
            
		}
        if (timetochange && !rb.Velocity.IsNearlyZero()) 
        {
            rb.Velocity = Vector3.Right  * speed; 
        }

	}
	public void StartMoveForward() // Rigidbody moves forward at speed //
	{
		rb.Velocity = Vector3.Forward  * speed;
		Log.Info( rb.Velocity);	 

        alreadymoved = true;
	}

}
