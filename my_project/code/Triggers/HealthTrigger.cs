using Sandbox;
using System;

public sealed class HealthTrigger : Component, Component.ITriggerListener

{
	[Property] float Amount { get; set; } = 10f;

	public void OnTriggerEnter( Collider other )
	{
		var player = other.Components.Get<PlayerMovement>();
		if ( player != null )
		{
			player.Health += Amount;
			player.Health = Math.Clamp( player.Health, 0, player.MaxHealth );
			Sound.Play("sounds/hurt.sound");

			if ( player.Health <= player.MinHealth )
			{
				Scene.LoadFromFile("scenes/youdied.scene"); 
				Sound.Play("sounds/fart.sound");
			}
		}
		
	}

	public void OnTriggerExit( Collider other )
	{
		
	}
}