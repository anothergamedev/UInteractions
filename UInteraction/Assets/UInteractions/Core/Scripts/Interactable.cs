using UnityEngine;
using System.Collections.Generic;

public class Interactable : MonoBehaviour 
{
	/// <summary>
	/// Key we are going to use to change the state of the object.
	/// </summary>
	public KeyCode Key = KeyCode.F;
	/// <summary>
	/// Indicates if this object can be interacted with.
	/// </summary>
	public bool CanBeInteractedWith = true;
	/// <summary>
	/// Indicates what the state of this object is going to be when the game starts.
	/// </summary>
	public bool StartState = false;
	/// <summary>
	/// Indicates if the object is controlled by the state of a external generator.
	/// </summary>
	public bool ControlledOnlyByGenerator = false;
	/// <summary>
	/// Indicates if this is the object that controls the input from the player.
	/// </summary>
	public bool ControlsItself = false;
	/// <summary>
	/// Indicates if this object can get controlled by some other external means.
	/// </summary>
	public bool CanGetControlledExternally = false;
    public bool IsSingleStateInteractable = false;
	/// <summary>
	/// Indicates the state at wich the object currently is at.
	/// </summary>
	public bool State = false;

    public bool IsGenerator = false;
	/// <summary>
	/// The list of interactable objects that are connected to this generator.
	/// </summary>
	public Interactable[] ConnectedInteractables;

	/// <summary>
	/// Indicates if we should log a message to the console when this object changes it's state.
	/// </summary>
	public bool DebugStateChange = false;

    /// <summary>
    /// The array of actions that will happen when the state of the object turns true.
    /// </summary>
    public List<Action> OnStateTrueActions;
    /// <summary>
    /// The size of the OnStateTrueActions array. (Used in the editor script)
    /// </summary>
    public int OnStateTrueActionsArraySize;
    /// <summary>
    /// The array of actions that will happen when the state of the object turns false.
    /// </summary>
    public List<Action> OnStateFalseActions;
    /// <summary>
    /// The size of the OnStateFalseActions array. (Used in the editor script)
    /// </summary>
    public int OnStateFalseActionsArraySize;

    public Item ItemData;

    private void Awake()
	{
        if (!IsGenerator)//If this is not a generator.
        {
            if(!IsSingleStateInteractable)
            {
                OnStateChange(StartState);//We change the starting state to match the preference of the user.
            }
        }
	}

	private void Update()
	{
		if (!ControlledOnlyByGenerator)
		{
			if (ControlsItself)
			{
				if (Input.GetKeyDown (Key))//If we press the key that changes it's state.
				{
					OnStateChange (!State);//Change the state to the complete opposite(If it's true, goes to false)
				}	
			}
		}
	}

	public void OnStateChange(bool newState)
	{
		if (DebugStateChange)//If we want to debug our state change.
		{
			Debug.Log (transform.name + " " + "has changed it's state to: " + "state_" + newState);
		}

        State = newState;//Makes sure we set the state to the given one.

        List<Action> arrayToExecute = (IsSingleStateInteractable) ? OnStateTrueActions : (State == true) ? OnStateTrueActions : OnStateFalseActions;

        if (arrayToExecute.Count > 0)
        {
            for (int i = 0; i < arrayToExecute.Count; i++)
            {
                if (arrayToExecute[i].Component == Components.Animator)
                {
                    if(arrayToExecute[i].AnimatorComponent)
                    {
                        if (arrayToExecute[i].AnimatorAction == AnimatorActions.SetBool)
                        {
                            arrayToExecute[i].AnimatorComponent.SetBool(arrayToExecute[i].StringField, arrayToExecute[i].BooleanField);
                        }
                        else if (arrayToExecute[i].AnimatorAction == AnimatorActions.SetFloat)
                        {
                            arrayToExecute[i].AnimatorComponent.SetFloat(arrayToExecute[i].StringField, arrayToExecute[i].FloatField);
                        }
                        else if (arrayToExecute[i].AnimatorAction == AnimatorActions.SetInt)
                        {
                            arrayToExecute[i].AnimatorComponent.SetInteger(arrayToExecute[i].StringField, arrayToExecute[i].IntegerField);
                        }
                        else if(arrayToExecute[i].AnimatorAction == AnimatorActions.SetTrigger)
                        {
                            arrayToExecute[i].AnimatorComponent.SetTrigger(arrayToExecute[i].StringField);
                        }
                        else if (arrayToExecute[i].AnimatorAction == AnimatorActions.SetEnabled)
                        {
                            arrayToExecute[i].AnimatorComponent.enabled = arrayToExecute[i].BooleanField;
                        }
                    }
                    else
                        Debug.LogError("There is no light component assigned at the action " + i + " on the " + transform.name + " GameObject");
                }
                else if (arrayToExecute[i].Component == Components.Light)
                {
                    if (arrayToExecute[i].LightComponent)
                    {
                        if (arrayToExecute[i].LightAction == LightActions.SetIntensity)
                        {
                            arrayToExecute[i].LightComponent.intensity = arrayToExecute[i].FloatField;
                        }
                        else if (arrayToExecute[i].LightAction == LightActions.SetRange)
                        {
                            arrayToExecute[i].LightComponent.range = arrayToExecute[i].FloatField;
                        }
                        else if (arrayToExecute[i].LightAction == LightActions.SetSpotAngle)
                        {
                            arrayToExecute[i].LightComponent.spotAngle = arrayToExecute[i].FloatField;
                        }
                        else if (arrayToExecute[i].LightAction == LightActions.SetType)
                        {
                            arrayToExecute[i].LightComponent.type = arrayToExecute[i].LightTypeField;
                        }
                        else if (arrayToExecute[i].LightAction == LightActions.SetColor)
                        {
                            arrayToExecute[i].LightComponent.color = arrayToExecute[i].ColorField;
                        }
                        else if (arrayToExecute[i].LightAction == LightActions.SetEnabled)
                        {
                            arrayToExecute[i].LightComponent.enabled = arrayToExecute[i].BooleanField;
                        }
                    }
                }
                else if (arrayToExecute[i].Component == Components.AudioSource)
                {
                    if (arrayToExecute[i].AudioSourceComponent)
                    {
                        if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.SetClip)
                        {
                            arrayToExecute[i].AudioSourceComponent.clip = arrayToExecute[i].AudioClipField;
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.Play)
                        {
                            arrayToExecute[i].AudioSourceComponent.Play();
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.Pause)
                        {
                            arrayToExecute[i].AudioSourceComponent.Pause();
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.UnPause)
                        {
                            arrayToExecute[i].AudioSourceComponent.UnPause();
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.Stop)
                        {
                            arrayToExecute[i].AudioSourceComponent.Stop();
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.SetLoop)
                        {
                            arrayToExecute[i].AudioSourceComponent.loop = arrayToExecute[i].BooleanField;
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.SetMute)
                        {
                            arrayToExecute[i].AudioSourceComponent.mute = arrayToExecute[i].BooleanField;
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.SetVolume)
                        {
                            arrayToExecute[i].AudioSourceComponent.volume = arrayToExecute[i].FloatField;
                        }
                        else if (arrayToExecute[i].AudioSourceAction == AudioSourceActions.SetEnabled)
                        {
                            arrayToExecute[i].AudioSourceComponent.enabled = arrayToExecute[i].BooleanField;
                        }
                    }
                    else
                        Debug.LogError("There is no audio source component assigned at the action " + i + " on the " + transform.name + " GameObject");
                }
                else if(arrayToExecute[i].Component == Components.Inventory)
                {
                    if(arrayToExecute[i].InventoryComponent)
                    {
                        if(arrayToExecute[i].InventoryAction == InventoryActions.AddItem)
                        {
                            arrayToExecute[i].InventoryComponent.AddItem(arrayToExecute[i].ItemField);
                        }
                        else if(arrayToExecute[i].InventoryAction == InventoryActions.SetEnabled)
                        {
                            arrayToExecute[i].InventoryComponent.enabled = arrayToExecute[i].BooleanField;
                        }
                    }
                    else
                        Debug.LogError("There is no inventory component assigned at the action " + i + " on the " + transform.name + " GameObject");
                }
                else if(arrayToExecute[i].Component == Components.GameObject)
                {
                    if(arrayToExecute[i].GameObjectComponent)
                    {
                        if(arrayToExecute[i].GameObjectAction == GameObjectActions.Destroy)
                        {
                            Destroy(arrayToExecute[i].GameObjectComponent, arrayToExecute[i].FloatField);
                        }
                        else if(arrayToExecute[i].GameObjectAction == GameObjectActions.Instantiate)
                        {
                            Instantiate(arrayToExecute[i].GameObjectField, arrayToExecute[i].Vector3Field, Quaternion.identity);
                        }
                        else if(arrayToExecute[i].GameObjectAction == GameObjectActions.AddComponent)
                        {
                            switch(arrayToExecute[i].ComponentField)
                            {
                                case AddableComponents.Animator:
                                    arrayToExecute[i].GameObjectComponent.AddComponent<Animator>();
                                    break;
                                case AddableComponents.AudioSource:
                                    arrayToExecute[i].GameObjectComponent.AddComponent<AudioSource>();
                                    break;
                                case AddableComponents.Camera:
                                    arrayToExecute[i].GameObjectComponent.AddComponent<Camera>();
                                    break;
                                case AddableComponents.Inventory:
                                    arrayToExecute[i].GameObjectComponent.AddComponent<Inventory>();
                                    break;
                                case AddableComponents.Light:
                                    arrayToExecute[i].GameObjectComponent.AddComponent<Light>();
                                    break;
                                case AddableComponents.Rigidbody:
                                    arrayToExecute[i].GameObjectComponent.AddComponent<Rigidbody>();
                                    break;
                                case AddableComponents.Slot:
                                    arrayToExecute[i].GameObjectComponent.AddComponent<Slot>();
                                    break;
                            }
                        }
                        else if(arrayToExecute[i].GameObjectAction == GameObjectActions.SetActive)
                        {
                            arrayToExecute[i].GameObjectComponent.SetActive(arrayToExecute[i].BooleanField);
                        }
                    }
                    else
                        Debug.LogError("There is no GameObject component assigned at the action " + i + " on the " + transform.name + " GameObject");
                }
                else if(arrayToExecute[i].Component == Components.Camera)
                {
                    if(arrayToExecute[i].CameraComponent)
                    {
                        if (arrayToExecute[i].CameraAction == CameraActions.SetClearFlags)
                        {
                            arrayToExecute[i].CameraComponent.clearFlags = arrayToExecute[i].ClearFlagsField;
                        }
                        else if (arrayToExecute[i].CameraAction == CameraActions.SetBackgroundColor)
                        {
                            arrayToExecute[i].CameraComponent.backgroundColor = arrayToExecute[i].ColorField;
                        }
                        else if(arrayToExecute[i].CameraAction == CameraActions.SetFieldOfView)
                        {
                            arrayToExecute[i].CameraComponent.fieldOfView = arrayToExecute[i].FloatField;
                        }
                        else if(arrayToExecute[i].CameraAction == CameraActions.SetEnabled)
                        {
                            arrayToExecute[i].CameraComponent.enabled = arrayToExecute[i].BooleanField;
                        }
                    }
                    else
                        Debug.LogError("There is no Camera component assigned at the action " + i + " on the " + transform.name + " GameObject");
                }
            }
        }

        if (IsGenerator)//If this object is a generator.
        {
            if(ConnectedInteractables.Length > 0)//And it has some connected interactables.
            {
                for (int i = 0; i < ConnectedInteractables.Length; i++)//Loop through them.
                {
                    ConnectedInteractables[i].OnStateChange(State);//Change their state.
                }
            }
        }
    }
}