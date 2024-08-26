using Sandbox;

public sealed class Weapons : Component
{
	[Property] public PlayerMovement Player { get; set; }
	[Property] public CameraMovement Camera { get; set; }
	[Property] public Animation gun { get; set; }
	protected override void OnUpdate()
	{
		gun.IronSightsAnim();
		// Set our ammo states
		if (Player.Ammo <= 0) Player.OutofAmmo = true;
		else Player.OutofAmmo = false;

		if (Player.AmmoinMag <=0) Player.MagEmpty = true;
		else Player.MagEmpty = false;
		
		if(Input.Pressed("Reload") && !Player.OutofAmmo && Camera.IsFirstPerson && Player.AmmoinMag < 17)
		{
			gun.ReloadAnim();
			Reload();
		} 

		if(Input.Pressed("Attack1") && !Player.MagEmpty && Camera.IsFirstPerson) 
		{
			gun.ShootAnim();
			if (gun.Reloading) return;
			Player.AmmoinMag -= 1;
			Shoot();
		}
	}
	public void Shoot()
	{
		var camAngles = Camera.Transform.Rotation.Angles();
		var camera = Components.Get<CameraMovement>();
		var camForward = camAngles.ToRotation().Forward;
		var startPos = Camera.Transform.Position;
		var endPos = startPos + (camForward *10000);
		var camTrace = Scene.Trace.Ray(startPos, endPos )
				
		.WithoutTags( "player")
		.Size(5)
		// .HitTriggers()
		.Run();
					
		if(camTrace.Hit)
		{
			if (gun.Reloading) return;
			endPos = (camTrace.HitPosition);
			Log.Info($"Hit: {camTrace.GameObject} at {camTrace.EndPosition}");
			camTrace.Body.ApplyImpulseAt( camTrace.HitPosition, camTrace.Direction * 2000f * camTrace.Body.Mass.Clamp( 0, 200 ) );	
		}	
	}
	public void Reload()
	{
		Player.Ammo += Player.AmmoinMag;
		Player.AmmoinMag = 17;
		Player.Ammo -= 17;

		if (Player.Ammo + Player.AmmoinMag > 17) return;
		Player.AmmoinMag += Player.Ammo;
		Player.Ammo = 0;	
	}
}
