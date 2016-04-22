using UnityEngine;

[System.Serializable]
public class Action
{
    /// <summary>
    /// The type of component we are going to use for this action.
    /// </summary>
    public Components Component;
    /// <summary>
    /// The type of action the animator is going to do.
    /// </summary>
    public AnimatorActions AnimatorAction;
    /// <summary>
    /// The animator that is going to do the action.
    /// </summary>
    public Animator AnimatorComponent;
    /// <summary>
    /// The type of action the light is going to do.
    /// </summary>
    public LightActions LightAction;
    /// <summary>
    /// The light that is going to do the action.
    /// </summary>
    public Light LightComponent;
    /// <summary>
    /// The type of action the audio source is going to do.
    /// </summary>
    public AudioSourceActions AudioSourceAction;
    /// <summary>
    /// the audio source that is going to do the action.
    /// </summary>
    public AudioSource AudioSourceComponent;
    public InventoryActions InventoryAction;
    public Inventory InventoryComponent;
    public GameObjectActions GameObjectAction;
    public GameObject GameObjectComponent;
    public CameraActions CameraAction;
    public Camera CameraComponent;

    /// <summary>
    /// The field used for color values.
    /// </summary>
    public Color ColorField;
    /// <summary>
    /// The field used for integer values.
    /// </summary>
    public int IntegerField;
    /// <summary>
    /// The field used for float values.
    /// </summary>
    public float FloatField;
    /// <summary>
    /// The field used for string values.
    /// </summary>
    public string StringField;
    /// <summary>
    /// The field used for boolean values.
    /// </summary>
    public bool BooleanField;
    public GameObject GameObjectField;
    public Vector3 Vector3Field;

    /// <summary>
    /// The field used for lightType values.
    /// </summary>
    public LightType LightTypeField;
    public AudioClip AudioClipField;
    public Item ItemField;
    public CameraClearFlags ClearFlagsField;
    public AddableComponents ComponentField;
}

public enum AnimatorActions
{
    SetBool, SetFloat, SetInt, SetTrigger, SetEnabled
}

public enum LightActions
{
    SetIntensity, SetRange, SetSpotAngle, SetType, SetColor, SetEnabled
}

public enum AudioSourceActions
{
    SetClip, Play, Stop, Pause, UnPause, SetLoop, SetVolume, SetMute, SetEnabled
}

public enum InventoryActions
{
    AddItem, SetEnabled
}

public enum GameObjectActions
{
    Destroy, Instantiate, AddComponent, SetActive
}

public enum CameraActions
{
    SetClearFlags, SetBackgroundColor, SetFieldOfView, SetEnabled
}

public enum Components
{
    Animator, Light, AudioSource, Inventory, GameObject, Camera
}

public enum AddableComponents
{
    Animator, Light, AudioSource, Inventory, Slot, Camera, Rigidbody
}