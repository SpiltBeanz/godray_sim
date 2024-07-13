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
			
			Player.Ammo += Player.AmmoinMag;
			Player.AmmoinMag = 17;
			Player.Ammo -= 17;

			if (Player.Ammo + Player.AmmoinMag > 17) return;
			Player.AmmoinMag += Player.Ammo;
			Player.Ammo = 0;	
		} 

		if(Input.Pressed("Attack1") && !Player.MagEmpty && Camera.IsFirstPerson) 
		{
			gun.ShootAnim();
			if (gun.Reloading) return;
			Player.AmmoinMag -= 1;
		}
	}
}
