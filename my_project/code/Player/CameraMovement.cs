using Sandbox;
using Sandbox.Citizen;

public sealed class CameraMovement : Component
{
	// Properties
	[Property] public PlayerMovement Player { get; set; }
	[Property] public GameObject Body { get; set; }
	[Property] public GameObject Head { get; set; }
	[Property] public GameObject viewmodelcamera { get; set; }
	[Property] public GameObject Crosshair { get; set; }
	[Property, Range (0f, 1000f, 0f)] public float Distance { get; set; } = 0f;
	// Variables
	public bool IsFirstPerson => Distance == 0f;
	public bool HasClothes;
	public bool HasGun;
	private Vector3 CurrentOffset = Vector3.Zero;
	private CameraComponent Camera;
	private ModelRenderer BodyRenderer;
	protected override void OnAwake()
	{
		Camera = Components.Get<CameraComponent>();
		BodyRenderer = Body.Components.Get<ModelRenderer>();
		Crosshair.Enabled = false;
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
		var targetOffset = Vector3.Up * 6;
		if(Player.IsCrouching) targetOffset += Vector3.Down * 32f;
		CurrentOffset = Vector3.Lerp( CurrentOffset, targetOffset, Time.Delta * 10f );

		// Set the position of the camera
		if (Input.MouseWheel.y < 0 )
		{
			if (Distance == 1000f) return;
			Distance += 50f;
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
				//If we are not in first person: //
				ApplyClothes();
				ApplyViewModel();
				
				viewmodelcamera.Enabled = false; 
				// Perform a trace backwards to see where we can safely place the camera
				var camForward = eyeAngles.ToRotation().Forward;
				var camTrace = Scene.Trace.Ray(camPos, camPos - (camForward * Distance) )
				
					.WithoutTags( "player", "trigger", "nocollide")
					.Size(5)
					.Run();
					
					if (camTrace.Hit) camPos = (camTrace.HitPosition + camTrace.Normal);
					else camPos = camTrace.EndPosition;
		
					// Show the body 
				BodyRenderer.RenderType = ModelRenderer.ShadowRenderType.On;
					// Gizmo draws for editor debugging. 
				var draw = Gizmo.Draw;
				if (Application.IsEditor && Input.Down("Reload"))
				{
					draw.Line(camPos, camPos - (camForward * -Distance));
					draw.LineCylinder(camPos, camPos - (camForward * -Distance), 5f,5f, 10);
				}		
			}
			else
			{
				//If we are in first person:
				ApplyClothes();
				ApplyViewModel();
				
				// Hide the body, show shadows//
				BodyRenderer.RenderType = ModelRenderer.ShadowRenderType.ShadowsOnly;
				// Hide the clothes/hair, show shadows//
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
	void ApplyClothes()
	{
		// Give player clothes for Mom
		if (Body.Components.TryGet<SkinnedModelRenderer>( out var model ) && !IsFirstPerson)
		{	
			if(HasClothes) return;
			else 
			{
				var clothing = ClothingContainer.CreateFromLocalUser();
				clothing.Apply(model);
				HasClothes = true;
				Log.Info($"Player has clothes: {HasClothes}");
			}	
		}
		else if(!HasClothes) return;
		else
		{
			HasClothes = false;	
			Log.Info($"Player has clothes: {HasClothes}");
		}
	}	
	void ApplyViewModel()
	{
		// Apply the view model (arms and guns)
		if (!IsFirstPerson)
		{	
			if(!HasGun) return;
			else 
			{
				HasGun = false;
				Log.Info($"Player has gun: {HasGun}");
				viewmodelcamera.Enabled = false; 
				Crosshair.Enabled = false;
			}
		}	
		else if(HasGun) return;
		else
		{
			HasGun = true;
			Log.Info($"Player has gun: {HasGun}");
			viewmodelcamera.Enabled = true; 
			Crosshair.Enabled = true;
		}		
	}	
}