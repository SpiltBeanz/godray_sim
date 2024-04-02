using System;
using System.Collections.Generic;
using Sandbox;
using Sandbox.Citizen;

public sealed class PlayerMovement : Component
{
	//Variables
	[Property] public float Health { get; set; } = 100f;
	[Property] public float MaxHealth { get; set; } = 100f;
	[Property] public float Armor { get; set; } = 0f;
	[Property] public float MaxArmor { get; set; } = 100f;
	[Property] public int Coins { get; set; } = 0;
	public TimeSince TimeAlive { get; set; } = 0f;

	[Property]

	public List<string> Items { get; set; } = new List<string>
	{
		"literaly anything"
	};

	public int ActiveSlot = 0;

	public int Slots => 9;

	// Movement Properties
    [Property] public float GroundControl { get; set; } = 4.0f;
	[Property] public float AirControl { get; set; } = 0.1f;
	[Property] public float MaxForce { get; set; } = 50f;
	[Property] public float Speed { get; set; } = 160f;
	[Property] public float RunSpeed { get; set; } = 290f;
	[Property] public float CrouchSpeed { get; set; } = 90f;
	[Property] public float JumpForce { get; set; } = 400f;

	// Object References
    [Property] public GameObject Head { get; set; }
    [Property] public GameObject Body { get; set; }

	// Member Variables
	public Vector3 WishVelocity = Vector3.Zero;
	public bool IsCrouching = false;
	public bool IsSprinting = false;
	private CharacterController characterController;
	private CitizenAnimationHelper animationHelper;


	protected override void OnAwake()
	{
		characterController = Components.Get<CharacterController>();
		animationHelper = Components.Get<CitizenAnimationHelper>();
	}
	// Give player clothes for Mom
	protected override void OnStart()
	{
		if (Body.Components.TryGet<SkinnedModelRenderer>( out var model ))
		{
			var clothing = ClothingContainer.CreateFromLocalUser();
			clothing.Apply(model);
		}
	}
	protected override void OnUpdate()
	{
		// Set our sprinting and crouching states
		UpdateCrouch();
		IsSprinting = Input.Down( "Run" );
		if (Input.Pressed("Jump")) Jump();

		// Update inputs
		if (Input.MouseWheel.y >= 0 )
		{
			ActiveSlot = (ActiveSlot + Math.Sign( Input.MouseWheel.y)) % Slots;
			
		}
		else if(Input.MouseWheel.y < 0 )
		{
			ActiveSlot = ((ActiveSlot + Math.Sign( Input.MouseWheel.y )) % Slots) + Slots;
		}

		RotateBody();
		UpdateAnimations();
	}

	protected override void OnFixedUpdate()
	{
		BuildWishVelocity();
		Move();
	}

	void BuildWishVelocity()
	{
		WishVelocity = 0;

		var rot = Head.Transform.Rotation;
		if(Input.Down("Forward")) WishVelocity += rot.Forward;
		if(Input.Down("Backward")) WishVelocity += rot.Backward;
		if(Input.Down("Left")) WishVelocity += rot.Left;
		if(Input.Down("Right")) WishVelocity += rot.Right;

		WishVelocity = WishVelocity.WithZ(0);
		if(!WishVelocity.IsNearZeroLength) WishVelocity = WishVelocity.Normal;

		if ( IsCrouching ) WishVelocity *= CrouchSpeed;
		else if ( IsSprinting ) WishVelocity *= RunSpeed;
		else WishVelocity *= Speed;
	}

	void Move()
	{
		// Get gravity from our scene
		var gravity = Scene.PhysicsWorld.Gravity;

		if(characterController.IsOnGround)
		{
			// Apply Friction/Acceleration
			characterController.Velocity = characterController.Velocity.WithZ(0);
			characterController.Accelerate( WishVelocity );
			characterController.ApplyFriction ( GroundControl );
		}
		else
		{
			// Apply air control / Gravity
			characterController.Velocity += gravity * Time.Delta * 0.5f;
			characterController.Accelerate( WishVelocity.ClampLength(MaxForce));
			characterController.ApplyFriction( AirControl );
		}

		// Move the character controller
		characterController.Move();

		// Apply the second half of gravity after movement
		if ( !characterController.IsOnGround )
		{
			characterController.Velocity += gravity * Time.Delta * 0.5f;
		}
		else
		{
			characterController.Velocity = characterController.Velocity.WithZ(0);
		}
		
	}

	void RotateBody()
	{
		if(Body is null) return;

		var targetAngle = new Angles(0, Head.Transform.Rotation.Yaw(), 0).ToRotation();
		float rotateDifference = Body.Transform.Rotation.Distance( targetAngle );

		if(rotateDifference > 50f || characterController.Velocity.Length > 10f)
		{
			Body.Transform.Rotation = Rotation.Lerp( Body.Transform.Rotation, targetAngle, Time.Delta * 2f );
		}
	}

	void Jump()
	{
		if(!characterController.IsOnGround) return;

		characterController.Punch(Vector3.Up * JumpForce);
		animationHelper?.TriggerJump();
	}

	void UpdateAnimations()
	{
		if(animationHelper is null) return;

		animationHelper.WithWishVelocity( WishVelocity);
		animationHelper.WithVelocity( characterController.Velocity );
		animationHelper.AimAngle = Head.Transform.Rotation;
		animationHelper.IsGrounded = characterController.IsOnGround;
		animationHelper.WithLook(Head.Transform.Rotation.Forward, 1f, 0.75f, 0.5f);
		animationHelper.MoveStyle = CitizenAnimationHelper.MoveStyles.Run;
		animationHelper.DuckLevel = IsCrouching ? 1f: 0f;
	}

	void UpdateCrouch()
	{
		if(characterController is null) return;

		if(Input.Pressed("Crouch") && !IsCrouching)
		{
			IsCrouching = true;
			characterController.Height /= 2f;
		}

		if(Input.Released("Crouch") && IsCrouching)
		{
			IsCrouching = false;
			characterController.Height *= 2f;
		}
	}
}