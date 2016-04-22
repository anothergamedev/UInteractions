using UnityEngine;

public class Interactor : MonoBehaviour 
{
	/// <summary>
	/// Indicates if the player is able to interact with objects.
	/// </summary>
	public bool CanInteract = true;
	/// <summary>
	/// Key we are going to use to interact with other objects.
	/// </summary>
	public KeyCode Key = KeyCode.E;
	/// <summary>
	/// The range at wich we can interact with other objects.
	/// </summary>
	public float Range = 10f;

	private void Update()
	{
		if (CanInteract)
			InteractionController ();
	}

	private void InteractionController()
	{
		Ray ray = Camera.main.ScreenPointToRay (new Vector2 (Screen.width / 2, Screen.height / 2));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Range))//Cast a ray from the middle of the screen with a range of Range.
		{
			if(Input.GetKeyDown(Key))//If we press the key we setup for interaction.
			{
				if (hit.transform.GetComponent<Interactable> ())//If we hit a Interactable object.
				{
					Interactable hitInteractable = hit.transform.GetComponent<Interactable> ();//Store it on a variable.

					if (hitInteractable.CanBeInteractedWith)//If the interactable we hit can be interacted with.
					{
                        if (hitInteractable.IsGenerator)//If it's a generator.
                            hitInteractable.OnStateChange(!hitInteractable.State);//Change it' state.
                        else //If not.
                        {
                            if (!hitInteractable.ControlledOnlyByGenerator)//If it's not controlled only by a generator.
                            {
                                if (hitInteractable.ControlsItself)//And can control itself.
                                {
                                    if (hitInteractable.CanGetControlledExternally)//But can also get controlled externally.
                                        hitInteractable.OnStateChange(!hitInteractable.State);//Change it's state.
                                }
                                else//If it doesn't control itself.
                                {
                                    hitInteractable.OnStateChange(!hitInteractable.State);  //Change it's state.
                                }
                            }
                        }
                    }
				}
			}
		}
	}
}
