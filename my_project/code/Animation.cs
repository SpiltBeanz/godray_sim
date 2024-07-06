using Sandbox;

public sealed class Animation : Component
{
	[Property] public SkinnedModelRenderer gun { get; set; }
	public bool reloading {get; set;} = false; 
	protected override void OnUpdate()
	{
		var IsAiming = Input.Down( "attack2" );
        gun.Set( "ironsights", IsAiming ? 2 : 0 );
        gun.Set( "ironsights_fire_scale", IsAiming ? 0.5f : 0f );
		
		if(Input.Pressed("Attack1"))
		{	
			if (reloading) return;
			gun.Set( "b_attack", true );
			Sound.Play("sounds/shoot.sound");
		}	
		if(Input.Pressed("Reload"))
		{	
			gun.Set( "b_reload", true );
			reloading = true;
			ReloadTime();
		}
	}
	async void ReloadTime()
	{
		await Task.DelaySeconds(1.2f);
		reloading = false;
	}
}
