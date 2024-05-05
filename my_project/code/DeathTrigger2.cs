using Sandbox;
using System;


public sealed class DeathTrigger2 : Component, Component.ICollisionListener

{
	private async void SceneLoad3()
    {
        await Task.DelaySeconds(.01f);
        Scene.LoadFromFile("scenes/godray3.scene");
		Sound.Play("sounds/fart.sound");
    }
	
	public void OnCollisionStart(Collision other)
	{
		 if(other.Other.GameObject.Tags.Has("player"))
        {
        Log.Warning(other.Other.GameObject.Name);
        // Scene.LoadFromFile("scenes/mainmenu.scene");
        SceneLoad3();
        }
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
