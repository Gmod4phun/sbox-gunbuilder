using Sandbox;

public sealed class LookAtLight : Component
{
	[Property] public GameObject Target { get; set; }

	protected override void OnUpdate()
	{
		GameObject.Transform.Rotation = Rotation.LookAt( (Target.Transform.Position - Transform.Position).Normal, Vector3.Up );
	}
}
