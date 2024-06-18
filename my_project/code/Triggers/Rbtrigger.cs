using Sandbox;

public sealed class Rbtrigger : Component, Component.ITriggerListener
{
	public bool istouchingplatform { get; set; } 
	public void OnTriggerEnter( Collider other )

	{
		var player = other.Components.Get<PlayerMovement>();
		

		 if(other.GameObject.Tags.Has("player") && player != null)
		{
			
			istouchingplatform = true;
			Log.Info ( istouchingplatform);
			player.GameObject.Tags.Add("onplatform");
			
		}

	}
	public void OnTriggerExit ( Collider other )
	{
		
		 var player = other.Components.Get<PlayerMovement>();
		 
		 if(other.GameObject.Tags.Has("player") && player != null)
		{
			istouchingplatform = false;
			Log.Info ( istouchingplatform );
			player.GameObject.Tags.Remove("onplatform");
			
		}

	}

}
