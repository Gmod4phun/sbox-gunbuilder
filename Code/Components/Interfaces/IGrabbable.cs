/// <summary>
/// Something that is grabbable. Implementing this on a Component will make it so the player's hands
/// will respond to grab events while you're colliding with it.
/// </summary>
public interface IGrabbable : IValid
{
	/// <summary>
	/// This grabbable has stopped being grabbed by a hand.
	/// </summary>
	/// <param name="hand"></param>
	public bool StopGrabbing( Hand hand );

	/// <summary>
	/// This grabbable thas started being grabbed by a hand.
	/// </summary>
	public bool StartGrabbing( Hand hand );

	/// <summary>
	/// Can we start grabbing?
	/// </summary>
	/// <param name="interactable"></param>
	/// <param name="hand"></param>
	/// <returns></returns>
	public bool CanStartGrabbing( BaseInteractable interactable, Hand hand )
	{
		return true;
	}

	/// <summary>
	/// Can we stop grabbing this?
	/// </summary>
	/// <param name="interactable"></param>
	/// <param name=""></param>
	/// <param name="hand"></param>
	/// <returns></returns>
	public bool CanStopGrabbing( BaseInteractable interactable, Hand hand )
	{
		return true;
	}

	/// <summary>
	/// The grabbable's GameObject.
	/// </summary>
	GameObject GameObject { get; }

	/// <summary>
	/// The tags on this grabbable.
	/// </summary>
	ITagSet Tags { get; }

	/// <summary>
	/// Hand (if we have one)
	/// </summary>
	Hand Hand { get; }

	/// <summary>
	/// Is this grabbable held by something already?
	/// </summary>
	bool IsHeld { get; }

	/// <summary>
	/// The interactable
	/// </summary>
	BaseInteractable Interactable { get; }

	/// <summary>
	/// The input type.
	/// </summary>
	public GrabInputType GrabInput { get; }

	/// <summary>
	/// A grab listener. This is a component that listens to grab events from anything in the world.
	/// Good for responding to events.
	/// </summary>
	public interface IGrabListener
	{
		/// <summary>
		/// Called when anything in the world has started being grabbed.
		/// </summary>
		/// <param name="interactable"></param>
		/// <param name="hand"></param>
		void OnGrabStart( BaseInteractable interactable, Hand hand ) { }

		/// <summary>
		/// Called when anything in the world has stopped being grabbed.
		/// </summary>
		/// <param name="interactable"></param>
		/// <param name="hand"></param>
		void OnGrabEnd( BaseInteractable interactable, Hand hand ) { }
	}
}

/// <summary>
/// What's our grabbing input type?
/// </summary>
public enum GrabInputType
{
	Grip,
	Trigger,
	Hover
}
