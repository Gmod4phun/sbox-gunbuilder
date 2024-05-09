

/// <summary>
/// A pouch on the player. It can hold interactables.
/// </summary>
public partial class PlayerPouch : Component, Component.ITriggerListener, IGrabbable.IGrabListener
{
	/// <summary>
	/// The player in question.
	/// </summary>
	[Property] public Player Player { get; set; }

	/// <summary>
	/// The list of objects held in this pouch.
	/// </summary>
	[Property] public BaseInteractable HeldInteractable { get; set; }

	/// <summary>
	/// How thick is our radius? TODO: derive from collider
	/// </summary>
	[Property] public float PouchRadius { get; set; } = 4;

	/// <summary>
	/// Is the duplicate mode on? If on, it'll duplicate the held interactable instead of taking it.
	/// </summary>
	[Property] public bool CanUseDuplicateMode { get; set; } = false;

	protected bool IsDuplicateModeOn { get; set; } = false;

	void IGrabbable.IGrabListener.OnGrabStart( BaseInteractable interactable, Hand hand )
	{
		if ( interactable == HeldInteractable )
		{
			HeldInteractable = null;
		}
	}

	void IGrabbable.IGrabListener.OnGrabEnd( BaseInteractable interactable, Hand hand )
	{
		// Can't insert new.
		if ( HeldInteractable.IsValid() )
		{
			return;
		}

		Log.Info( $"Stopped grabbing {interactable}" );

		// Did we drop something in the vicinity of the pouch?
		if ( interactable.Transform.Position.Distance( Transform.Position ) < PouchRadius )
		{
			HeldInteractable = interactable;
			HeldInteractable.Rigidbody.MotionEnabled = false;
		}
	}

	protected override void OnUpdate()
	{
		if ( HeldInteractable.IsValid() )
		{
			HeldInteractable.Transform.Position = Transform.Position;
			HeldInteractable.Transform.Rotation = Transform.Rotation;
		}
	}
}
