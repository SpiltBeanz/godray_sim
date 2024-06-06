using Sandbox;

public sealed class BouncePad : Component, Component.ICollisionListener
{
	[Property] float Bounciness {get; set;} = 10f;
	public void OnCollisionStart(Collision other)
	{
		var characterController = other.Other.GameObject.Components.Get<CharacterController>();
		if (characterController != null )
		{
			characterController.Punch (Vector3.Up * Bounciness) ;
		}
	}

	public void OnCollisionUpdate(Collision other)
	{
		
	}

	public void OnCollisionStop(CollisionStop other)
	{
		
	}

}