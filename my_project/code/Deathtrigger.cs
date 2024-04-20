using Sandbox;
using System;


public sealed class DeathTrigger : Component, Component.ICollisionListener

{
	// [Property] float Amount { get; set; } = 0f;
	
	public void OnCollisionStart(Collision other)
	{
		
		// var player = other.Other.GameObject.Components.Get<PlayerMovement>();
		// if ( player != null )
		// {
		// 	player.Health += Amount;
		// 	player.Health = Math.Clamp( player.Health, 0, player.MaxHealth );
		// 	player.Armor += Amount;
		// 	player.Armor = Math.Clamp( player.Armor, 0, player.MaxArmor );

			
		// }
		
		Sound.Play("sounds/hurt.sound");

		Scene.LoadFromFile("scenes/godray2.scene");
	}

	public void OnCollisionUpdate(Collision other)
	{
		Log.Info(other);
	}

	public void OnCollisionStop(CollisionStop other)
	{
		Log.Info(other);
	}

}

