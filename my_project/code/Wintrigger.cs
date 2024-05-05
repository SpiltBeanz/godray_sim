using Sandbox;
using System;


public sealed class Wintrigger : Component, Component.ICollisionListener

{
	
	public void OnCollisionStart(Collision other)
	{
		
		
        Scene.LoadFromFile("scenes/youwin.scene");
		Sound.Play("sounds/hurt.sound");

		
		
    	
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