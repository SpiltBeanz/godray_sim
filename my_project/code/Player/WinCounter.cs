using Sandbox;

public sealed class WinCounter : Component
{
	protected override void OnAwake()
	{
		Sandbox.Services.Stats.Increment( "games-won", 1 );
		Log.Info("You Win!");
	}
}