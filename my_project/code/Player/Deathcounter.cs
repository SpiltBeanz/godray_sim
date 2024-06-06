using Sandbox;

public sealed class DeathCounter : Component
{
	protected override void OnAwake()
	{
		Sandbox.Services.Stats.Increment( "died", 1 );
		Log.Info("You Diead!");
	}
}