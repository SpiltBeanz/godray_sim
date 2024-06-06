using Sandbox;
using System;


public sealed class DeathTrigger : Component, Component.ICollisionListener



{
    private async void SceneLoad()
    {
        await Task.DelaySeconds(.01f);
        Sandbox.Services.Stats.Increment( "died", 1 );
		Log.Info("You Diead!");
        Scene.LoadFromFile("scenes/godray2.scene");
        Sound.Play("sounds/fart.sound");
    }
   
    public void OnCollisionStart(Collision other)
    {

       if(other.Other.GameObject.Tags.Has("player"))
        {
        Log.Warning(other.Other.GameObject.Name);
        // Scene.LoadFromFile("scenes/mainmenu.scene");
        SceneLoad();
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

// using Sandbox;
// using System;
// public sealed class DeathTrigger : Component, Component.ITriggerListener
// {

// 	public void OnTriggerEnter( Collider other )
// 	{
		
//         var player = other.Components.Get<PlayerMovement>();
// 		if ( player != null )
//         {
//         other.GameObject.Destroy();
//         Log.Warning(other.GameObject.Name);
//         Sound.Play("sounds/fart.sound");
//         Scene.LoadFromFile("scenes/mainmenu.scene");

//         }

		

// 	}

// 	public void OnTriggerExit( Collider other )
// 	{
		
// 	}
// }