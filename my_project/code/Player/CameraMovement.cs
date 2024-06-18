using Sandbox;

public sealed class CameraMovement : Component
{
	// Properties
	[Property] public PlayerMovement Player { get; set; }
	[Property] public GameObject Body { get; set; }
	[Property] public GameObject Head { get; set; }
	[Property, Range (0f, 1000f, 0f)] public float Distance { get; set; } = 0f;

	// Variables

	public bool IsFirstPerson => Distance == 0f;
	private Vector3 CurrentOffset = Vector3.Zero;
	private CameraComponent Camera;
	private ModelRenderer BodyRenderer;
	protected override void OnAwake()
	{
		Camera = Components.Get<CameraComponent>();
		BodyRenderer = Body.Components.Get<ModelRenderer>();
	}

	protected override void OnUpdate()
	{
		// Rotate the head base on mouse movement
		var eyeAngles = Head.Transform.Rotation.Angles();
		eyeAngles.pitch += Input.MouseDelta.y * 0.1f;
		eyeAngles.yaw -= Input.MouseDelta.x * 0.1f;
		eyeAngles.roll = 0f;
		eyeAngles.pitch = eyeAngles.pitch.Clamp(-89.9f, 89.9f );
		Head.Transform.Rotation = eyeAngles.ToRotation();

		// Set the current camera offset
		var targetOffset = Vector3.Zero;
		if(Player.IsCrouching) targetOffset += Vector3.Down * 32f;
		CurrentOffset = Vector3.Lerp( CurrentOffset, targetOffset, Time.Delta * 10f );

		// Set the position of the camera
		if (Input.MouseWheel.y < 0 )
		{
			if (Distance == 1000f) return;
			Distance += 50f;
			GiveClothes();
		}
		else if (Input.MouseWheel.y > 0 )
			{
				if (Distance <= 0) return;
				Distance -= 50f;
			}
		

		if(Camera is not null)
		{
			var camPos = Head.Transform.Position + CurrentOffset;
			if(!IsFirstPerson)
			{
				// Perform a trace backwards to see where we can safely place the camera
				var camForward = eyeAngles.ToRotation().Forward;
				var camTrace = Scene.Trace.Ray(camPos, camPos - (camForward * Distance))
					.WithoutTags( "player", "trigger")
					.Run();

					if(camTrace.Hit)
					{
						camPos = camTrace.HitPosition + camTrace.Normal;
					}
					else
					{
						camPos = camTrace.EndPosition;
					}

					// Show the body if not in first person
					BodyRenderer.RenderType = ModelRenderer.ShadowRenderType.On;
			}
			else
			{
				// Hide the body if in first person
				BodyRenderer.RenderType = ModelRenderer.ShadowRenderType.ShadowsOnly;
				// Hide the clothes/hair if in first person
				var renderType = (!IsProxy && IsFirstPerson) ? ModelRenderer.ShadowRenderType.ShadowsOnly : ModelRenderer.ShadowRenderType.On;
					foreach (var modelRenderer in Body.Components.GetAll<ModelRenderer>())
						{
    						modelRenderer.RenderType = renderType;
						}
			}

			// Set the position of the camera to our calc position
			Camera.Transform.Position = camPos;
			Camera.Transform.Rotation = eyeAngles.ToRotation();

		}
	}
	void GiveClothes()
	{
		// Give player clothes for Mom
		if (Body.Components.TryGet<SkinnedModelRenderer>( out var model ))
		{
			var clothing = ClothingContainer.CreateFromLocalUser();
			clothing.Apply(model);
		}
	}
}