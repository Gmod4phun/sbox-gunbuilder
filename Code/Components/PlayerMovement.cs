public partial class PlayerMovement : Component
{
	[Property] public Hand Hand { get; set; }
	[Property] public GameObject Head { get; set; }

	[Property] public float Speed { get; set; } = 50f;

	protected override void OnUpdate()
	{
		var controller = Hand.GetController();
		var inputDir = controller != null ? controller.Joystick.Value : Vector2.Zero;
		var headRot = Head.Transform.Rotation;
		var fwd = new Vector3( inputDir.y, -inputDir.x, 0 ) * headRot;

		Move( fwd );
	}

	void Move( Vector3 direction )
	{
		direction = direction.WithZ( 0 );
		var velocity = direction * Speed * Time.Delta;
		GameObject.Transform.Position += velocity;
	}
}