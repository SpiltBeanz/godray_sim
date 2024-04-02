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
		}
	}

	public void OnTriggerExit( Collider other )
	{
		
	}
}