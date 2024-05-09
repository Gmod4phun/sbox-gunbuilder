public partial class GrabPoint
{
	GameObject IGrabbable.GameObject => GameObject;
	ITagSet IGrabbable.Tags => Tags;
	Hand IGrabbable.Hand => HeldHand;
	bool IGrabbable.IsHeld => HeldHand.IsValid();

	bool IGrabbable.StartGrabbing( Hand hand )
	{
		if ( Interactable?.Interact( hand, this ) ?? false )
		{
			HeldHand = hand;
			OnStartGrabbing( hand );

			return true;
		}

		return false;
	}

	bool IGrabbable.StopGrabbing( Hand hand )
	{
		if ( Interactable?.StopInteract( this ) ?? false )
		{
			Log.Info( "On Stop Grabbing" );
			HeldHand = null;
			OnStopGrabbing( hand );

			return true;
		}

		return false;
	}

	bool IGrabbable.CanStartGrabbing( BaseInteractable interactable, Hand hand )
	{
		return true;
	}

	bool IGrabbable.CanStopGrabbing( BaseInteractable interactable, Hand hand )
	{
		return true;
	}
}
