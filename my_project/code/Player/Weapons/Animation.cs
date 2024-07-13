using Sandbox;

public sealed class Animation : Component
{
	[Property] public SkinnedModelRenderer gun { get; set; }
	[Property] public PlayerMovement Player { get; set; }
	public bool Reloading {get; set;} = false; 

	public void ShootAnim()
	{
		if (Reloading) return;
		gun.Set( "b_attack", true );
		Sound.Play("sounds/shoot.sound");
	}
	public void ReloadAnim()
	{
		gun.Set( "b_reload", true );
		Reloading = true;
		ReloadTime();
	}
	async void ReloadTime()
	{
		//Delay our time between reloads. 
		await Task.DelaySeconds(1.2f);
		Reloading = false;
	}
	public void IronSightsAnim()
	{
		// On right click aim down sights. 
		var IsAiming = Input.Down( "attack2" );
        gun.Set( "ironsights", IsAiming ? 2 : 0 );
        gun.Set( "ironsights_fire_scale", IsAiming ? 0.5f : 0f );
	}
}
