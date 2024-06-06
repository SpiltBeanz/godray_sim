using Sandbox;
public sealed class PlatMover : Component

{
    [Property] public Vector3 MoveDistance;
    [Property] public float TimeToMove;
    [Property] public bool ReverseAnimation;

    private Vector3 start, end;
    private TimeUntil animDuration;

    protected override void OnStart()
    {
        start = Transform.Position;
        end = start + MoveDistance;
        animDuration = TimeToMove;
    }
    protected override void OnFixedUpdate()
    {
        if(ReverseAnimation && animDuration.Fraction == 1)
        {
            var temp = end;
            end = start;
            start = temp;
            animDuration = TimeToMove;
        }
        Transform.Position = start.LerpTo( end, animDuration.Fraction );
    }
}

// public sealed class Platmover : Component

// {
// 	[Property] public float Speed { get; set; } = 100f;
// 	[Property] private new Vector3 start;
// 	[Property] private new Vector3 end;
	
// 	// Vector3 start =  new Vector3(x,y,z);
// 	// Vector3 end = Vector3.One;
// 	TimeUntil animDuration = 5;

// 	protected override void OnUpdate()

// 	{
// 		Transform.Position = Vector3.Lerp(startposition.transform.position, endposition.transform.position, Speed * Time.Delta)
// 	}

	
// }


    // private Rigidbody rb { get; set; }

    // protected override void OnFixedUpdate() {

    //     if(rb == null) return;

    //     if (Input.Down( "Forward" ))
    //         rb.Velocity += Vector3.Up * PaddleMoveSpeed * Time.Delta;

    //     if(Input.Down("Backward"))
    //         rb.Velocity += Vector3.Down * PaddleMoveSpeed * Time.Delta;

    //     if(Input.Released("Forward") || Input.Released("Backward"))
    //         rb.Velocity = Vector3.Zero;

	// protected override void OnUpdate()
	// {
	// 	MoveUp();
	// }


	// private async void MoveUp()
    // {
        
	// 	await Task.DelaySeconds(3f);
	// 	Transform.Position += Vector3.Up * Speed * Time.Delta;


    // }