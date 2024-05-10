
/// <summary>
/// A weapon's slide release system. At the moment, it kinda does too many jobs. Ideally, we'd have a slide/bolt component, and implement the release system entirely separately.
/// </summary>
public partial class SlideReleaseSystem : Component
{
	/// <summary>
	/// The point interactable belonging to this slide.
	/// </summary>
	[RequireComponent] PointInteractable PointInteractable { get; set; }

	/// <summary>
	/// It's a weapon, so what is it?
	/// </summary>
	[Property] public Weapon Weapon { get; set; }

	/// <summary>
	/// What grab point can control this slide?
	/// </summary>
	[Property] public GrabPoint InputGrabPoint { get; set; }

	/// <summary>
	/// Is the slide pulled back all the way?
	/// </summary>
	public bool IsPulled => PointInteractable.CompletionValue.AlmostEqual( 1f );

	/// <summary>
	/// The hand on the grab point (if we have one)
	/// </summary>
	public Hand Hand => InputGrabPoint?.HeldHand;

	protected override void OnStart()
	{
		// We want to know when the interactable completion value changes
		PointInteractable.OnCompletionValue += OnCompletionValue;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="before"></param>
	/// <param name="after"></param>
	public void OnCompletionValue( float before, float after )
	{
		if ( after == 0 )
		{
			// Feed from the magazine
			Weapon.TryFeedFromMagazine();
		}

		if ( after == 1 )
		{
			// We pulled all the way back, try to eject bullet from chamber if we have one.
			Weapon.TryEjectFromChamber();
		}
	}

	/// <summary>
	/// Pulls the slide back all the way programmatically.
	/// </summary>
	public void PullManually()
	{
		PointInteractable.CompletionValue = 1f;
	}


	protected override void OnUpdate()
	{
		if ( !Hand.IsValid() ) return;

		// Input checking for pressing the 'slide release' button.
		if ( Hand.GetController().ButtonA.IsPressed && IsPulled )
		{
			PointInteractable.CompletionValue = 0;
		}
	}
}
