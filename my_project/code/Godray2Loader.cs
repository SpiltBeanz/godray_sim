using Sandbox;

public sealed class Godray2Loader : Component
{
	protected override void OnStart()
	{
		Scene.LoadFromFile("scenes/godray2.scene");
	}
}