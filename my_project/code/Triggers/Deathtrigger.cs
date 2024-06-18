using Sandbox;
using System;

public sealed class DeathTrigger : Component, Component.ICollisionListener

{
    [Property] SceneFile activescene { get; set; }
    public void OnCollisionStart(Collision other)
    {

       if(other.Other.GameObject.Tags.Has("player"))
        {
        Log.Warning(other.Other.GameObject.Name);
        Restart();
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

        private async void Restart()
    {
        
        await Task.DelaySeconds(.01f);
        Sandbox.Services.Stats.Increment( "died", 1 );
		Log.Info("You Diead!");
        Scene.Load(activescene);
        Sound.Play("sounds/fart.sound");
    }
   
}

