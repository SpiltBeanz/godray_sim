using Sandbox;

public sealed class Animation : Component
{
	[Property] public SkinnedModelRenderer gun { get; set; }
	protected override void OnUpdate()
	{
		if(Input.Pressed("Attack1"))
		{	
			gun.Set( "b_attack", true );
			Sound.Play("sounds/shoot.sound");
		}	
		if(Input.Pressed("Reload"))
		{	
			gun.Set( "b_reload", true );
		}
	}
}
