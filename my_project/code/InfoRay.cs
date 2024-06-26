using Sandbox;
public sealed class InfoRay : Component
{
	[Property] public GameObject Camera { get; set; }

	protected override void OnUpdate()
    {
		var camAngles = Camera.Transform.Rotation.Angles();
		var camera = Components.Get<CameraMovement>();
		if(Camera is not null && Input.Down("Attack1"))
		{
			var camForward = camAngles.ToRotation().Forward;
			var startPos = Camera.Transform.Position;
			var endPos = startPos + (camForward *10000);
			var camTrace = Scene.Trace.Ray(startPos, endPos )
				
			.WithoutTags( "player")
			.Size(5)
			// .HitTriggers()
			.Run();
					
				if(camTrace.Hit && camera.IsFirstPerson)
				{
					endPos = (camTrace.HitPosition);
					// Gizmo draws for editor debugging.
					var draw = Gizmo.Draw;
					draw.Color = Color.Red;
					draw.LineThickness = 2;
					draw.Line(startPos + Vector3.Down, endPos);
					draw.LineCylinder(startPos, endPos + camTrace.Normal, 5f,5f, 10);	

					if(Input.Pressed("Attack1"))
					{
						Log.Info($"Hit: {camTrace.GameObject} at {camTrace.EndPosition}");
					}	
				}
			else if(Input.Pressed("Attack1"))
			{
				Log.Info("no trace hit");
			}	
		}
	}
}
