using Sandbox;

public sealed class DisableBeforeRender : Component
{
	protected override void OnPreRender()
	{
		GameObject.Enabled = false;
	}
}
