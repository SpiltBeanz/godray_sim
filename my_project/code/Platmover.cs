using Sandbox;
public sealed class PlatMover : Component

{
    [Property] public Vector3 MoveDistance;
    [Property] public float TimeToMove;
    [Property] public bool ReverseAnimation;

    private Vector3 start, end;
    private TimeUntil animDuration;

    protected override void OnStart()
    {
        start = Transform.Position;
        end = start + MoveDistance;
        animDuration = TimeToMove;
    }
    protected override void OnFixedUpdate()
    {
        if(ReverseAnimation && animDuration.Fraction == 1)
        {
            var temp = end;
            end = start;
            start = temp;
            animDuration = TimeToMove;
        }
        Transform.Position = start.LerpTo( end, animDuration.Fraction );
    }
}

