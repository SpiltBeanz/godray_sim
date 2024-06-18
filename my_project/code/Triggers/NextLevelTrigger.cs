using Sandbox;
using System;


public sealed class NextLevelTrigger : Component, Component.ICollisionListener

{
	[Property] SceneFile nextscene { get; set; }

	public void OnCollisionStart(Collision other)
	{
		 if(other.Other.GameObject.Tags.Has("player"))
        {
        Log.Warning(other.Other.GameObject.Name);
        NextLevel();
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

	private async void NextLevel()
    {
        await Task.DelaySeconds(.01f);
        Scene.Load(nextscene);
		Sound.Play("sounds/fart.sound");
    }

}
