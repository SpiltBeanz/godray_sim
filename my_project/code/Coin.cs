using Sandbox;
using System;
public sealed class Coin : Component, Component.ITriggerListener
{
[Property] float Amount { get; set; } = 25f;

	public void OnTriggerEnter( Collider other )
	{
		var player = other.Components.Get<PlayerMovement>();
		if ( player != null )
		{
			player.Coins += 1;
			player.Armor += Amount;
			player.Armor = Math.Clamp( player.Armor, 0, player.MaxArmor );
			GameObject.Destroy();
			Sound.Play("sounds/kenney/ui/ui.button.press.sound");
		}

	}

	public void OnTriggerExit( Collider other )
	{
		
	}
}