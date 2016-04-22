using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Interactable), true)]
[CanEditMultipleObjects]
public class InteractableEditor : Editor
{
    bool[] actionFoldoutsTrue = new bool[100];
    bool onStateTrueActionsFoldout;

    bool[] actionFoldoutsFalse = new bool[100];
    bool onStateFalseActionsFoldout;

    public override void OnInspectorGUI()
    {
        Interactable myTarget = (Interactable)target;

        GUIContent isGeneratorContent = new GUIContent("Is Generator", "Indicates if this object a generator that changes states for other objects.");
        myTarget.IsGenerator = EditorGUILayout.Toggle(isGeneratorContent, myTarget.IsGenerator);
        if (myTarget.IsGenerator)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            SerializedProperty property = serializedObject.FindProperty("ConnectedInteractables");
            EditorGUILayout.PropertyField(property, true);
            serializedObject.ApplyModifiedProperties();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }
        GUIContent canBeInteractedWithContent = new GUIContent("Can Be Interacted With", "Indicates if this object can be interacted with.");
        myTarget.CanBeInteractedWith = EditorGUILayout.Toggle(canBeInteractedWithContent, myTarget.CanBeInteractedWith);
        if (myTarget.CanBeInteractedWith)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            if (!myTarget.IsGenerator)
            {
                GUIContent startStateContent = new GUIContent("Start State", "Indicates what the state of this object is going to be when the game starts.");
                myTarget.StartState = EditorGUILayout.Toggle(startStateContent, myTarget.StartState);

                GUIContent controlledByGeneratorContent = new GUIContent("Controlled Only By Generator", "Indicates if the object is controlled by the state of a external generator. [Removes the option of activating the object without generator]");
                myTarget.ControlledOnlyByGenerator = EditorGUILayout.Toggle(controlledByGeneratorContent, myTarget.ControlledOnlyByGenerator);

                if (!myTarget.ControlledOnlyByGenerator)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    EditorGUILayout.BeginVertical();
                    GUIContent controlsItselfContent = new GUIContent("Controls Itself", "Indicates if this is the object that controls the input from the player.");
                    myTarget.ControlsItself = EditorGUILayout.Toggle(controlsItselfContent, myTarget.ControlsItself);
                    if (myTarget.ControlsItself)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(20);
                        EditorGUILayout.BeginVertical();
                        GUIContent keyContent = new GUIContent("KeyCode", "Key we are going to use to change the state of the object.");
                        myTarget.Key = (KeyCode)EditorGUILayout.EnumPopup(keyContent, myTarget.Key);
                        GUIContent canGetControlledExternallyContent = new GUIContent("Can Get Controlled Externally", "Indicates if this object can get controlled by some other external means.");
                        myTarget.CanGetControlledExternally = EditorGUILayout.Toggle(canGetControlledExternallyContent, myTarget.CanGetControlledExternally);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(0);
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(0);
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                GUIContent controlsItselfContent = new GUIContent("Controls Itself", "Indicates if this is the object that controls the input from the player.");
                myTarget.ControlsItself = EditorGUILayout.Toggle(controlsItselfContent, myTarget.ControlsItself);
                if (myTarget.ControlsItself)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    EditorGUILayout.BeginVertical();
                    GUIContent keyContent = new GUIContent("KeyCode", "Key we are going to use to change the state of the object.");
                    myTarget.Key = (KeyCode)EditorGUILayout.EnumPopup(keyContent, myTarget.Key);
                    GUIContent canGetControlledExternallyContent = new GUIContent("Can Get Controlled Externally", "Indicates if this object can get controlled by some other external means.");
                    myTarget.CanGetControlledExternally = EditorGUILayout.Toggle(canGetControlledExternallyContent, myTarget.CanGetControlledExternally);
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(0);
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
            GUIContent debugStateChangeContent = new GUIContent("Debug State Change", "Indicates if we should log a message to the console when this object changes it's state.");
            myTarget.DebugStateChange = EditorGUILayout.Toggle(debugStateChangeContent, myTarget.DebugStateChange);
        }

        GUIContent isSingleStateInteractableContent = new GUIContent("Is Single State Interactable", "Is this an object that does the same actions when it's pressed.");
        myTarget.IsSingleStateInteractable = EditorGUILayout.Toggle(isSingleStateInteractableContent, myTarget.IsSingleStateInteractable);

        onStateTrueActionsFoldout = EditorGUILayout.Foldout(onStateTrueActionsFoldout, (myTarget.IsSingleStateInteractable) ? "On Interact Actions" : "On State True Actions");
        if (onStateTrueActionsFoldout)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            GUIContent onStateTrueActionsArraySizeContent = new GUIContent("Size", "The size of this array.");
            myTarget.OnStateTrueActionsArraySize = EditorGUILayout.IntField(onStateTrueActionsArraySizeContent, myTarget.OnStateTrueActionsArraySize);
            myTarget.OnStateTrueActionsArraySize = Mathf.Clamp(myTarget.OnStateTrueActionsArraySize, 0, int.MaxValue);

            if (myTarget.OnStateTrueActions.Count != myTarget.OnStateTrueActionsArraySize)
            {
                if(myTarget.OnStateTrueActions.Count < myTarget.OnStateTrueActionsArraySize)
                {
                    for (int i = myTarget.OnStateTrueActions.Count; i < myTarget.OnStateTrueActionsArraySize; i++)
                    {
                        myTarget.OnStateTrueActions.Add(new Action());
                    }
                }
                else if(myTarget.OnStateTrueActions.Count > myTarget.OnStateTrueActionsArraySize)
                {
                    for (int i = myTarget.OnStateTrueActionsArraySize; i < myTarget.OnStateTrueActions.Count; i++)
                    {
                        myTarget.OnStateTrueActions.RemoveAt(i);
                    }
                }
            }

            for (int i = 0; i < myTarget.OnStateTrueActionsArraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                actionFoldoutsTrue[i] = EditorGUILayout.Foldout(actionFoldoutsTrue[i], "Element " + i);
                if (actionFoldoutsTrue[i])
                {
                    ActionsArrayShow(myTarget.OnStateTrueActions, i);
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }

        if (!myTarget.IsSingleStateInteractable)
        {
            onStateFalseActionsFoldout = EditorGUILayout.Foldout(onStateFalseActionsFoldout, "On State False Actions");
            if (onStateFalseActionsFoldout)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent onStateFalseActionsArraySizeContent = new GUIContent("Size", "The size of this array.");
                myTarget.OnStateFalseActionsArraySize = EditorGUILayout.IntField(onStateFalseActionsArraySizeContent, myTarget.OnStateFalseActionsArraySize);
                myTarget.OnStateFalseActionsArraySize = Mathf.Clamp(myTarget.OnStateFalseActionsArraySize, 0, 100);

                if (myTarget.OnStateFalseActions.Count != myTarget.OnStateFalseActionsArraySize)
                {
                    if (myTarget.OnStateFalseActions.Count < myTarget.OnStateFalseActionsArraySize)
                    {
                        for (int i = myTarget.OnStateFalseActions.Count; i < myTarget.OnStateFalseActionsArraySize; i++)
                        {
                            myTarget.OnStateFalseActions.Add(new Action());
                        }
                    }
                    else if (myTarget.OnStateFalseActions.Count > myTarget.OnStateFalseActionsArraySize)
                    {
                        for (int i = myTarget.OnStateFalseActionsArraySize; i < myTarget.OnStateFalseActions.Count; i++)
                        {
                            myTarget.OnStateFalseActions.RemoveAt(i);
                        }
                    }
                }

                for (int i = 0; i < myTarget.OnStateFalseActionsArraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    EditorGUILayout.BeginVertical();
                    actionFoldoutsFalse[i] = EditorGUILayout.Foldout(actionFoldoutsFalse[i], "Element " + i);
                    if (actionFoldoutsFalse[i])
                    {
                        ActionsArrayShow(myTarget.OnStateFalseActions, i);
                    }
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(0);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myTarget);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ActionsArrayShow(List<Action> array, int i)
    {
        GUIContent actionComponentContent = new GUIContent("Action Component", "The type of component we are going to use for this action.");
        array[i].Component = (Components)EditorGUILayout.EnumPopup(actionComponentContent, array[i].Component);
        if (array[i].Component == Components.Animator)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            GUIContent componentContent = new GUIContent("Component", "The component that is going to do the action.");
            array[i].AnimatorComponent = (Animator)EditorGUILayout.ObjectField(componentContent, array[i].AnimatorComponent, typeof(Animator), true);
            GUIContent actionContent = new GUIContent("Action", "The type of action the component is going to do.");
            array[i].AnimatorAction = (AnimatorActions)EditorGUILayout.EnumPopup(actionContent, array[i].AnimatorAction);
            if (array[i].AnimatorAction == AnimatorActions.SetBool)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterNameContent = new GUIContent("Parameter Name", "The name of the boolean that is going to be set.");
                array[i].StringField = EditorGUILayout.TextField(parameterNameContent, array[i].StringField);
                GUIContent parameterStateContent = new GUIContent("New Value", "The new value that the parameter is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AnimatorAction == AnimatorActions.SetFloat)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterNameContent = new GUIContent("Parameter Name", "The name of the boolean that is going to be set.");
                array[i].StringField = EditorGUILayout.TextField(parameterNameContent, array[i].StringField);
                GUIContent parameterStateContent = new GUIContent("New Value", "The new value that the parameter is going to have.");
                array[i].FloatField = EditorGUILayout.FloatField(parameterStateContent, array[i].FloatField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AnimatorAction == AnimatorActions.SetInt)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterNameContent = new GUIContent("Parameter Name", "The name of the boolean that is going to be set.");
                array[i].StringField = EditorGUILayout.TextField(parameterNameContent, array[i].StringField);
                GUIContent parameterStateContent = new GUIContent("New Value", "The new value that the parameter is going to have.");
                array[i].IntegerField = EditorGUILayout.IntField(parameterStateContent, array[i].IntegerField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AnimatorAction == AnimatorActions.SetTrigger)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterNameContent = new GUIContent("Parameter Name", "The name of the boolean that is going to be set.");
                array[i].StringField = EditorGUILayout.TextField(parameterNameContent, array[i].StringField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AnimatorAction == AnimatorActions.SetEnabled)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new enabled value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }
        else if (array[i].Component == Components.Light)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            GUIContent componentContent = new GUIContent("Component", "The component that is going to do the action.");
            array[i].LightComponent = (Light)EditorGUILayout.ObjectField(componentContent, array[i].LightComponent, typeof(Light), true);
            GUIContent actionContent = new GUIContent("Action", "The component that is going to do the action.");
            array[i].LightAction = (LightActions)EditorGUILayout.EnumPopup(actionContent, array[i].LightAction);
            if (array[i].LightAction == LightActions.SetIntensity)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new intensity value that the light is going to have.");
                array[i].FloatField = EditorGUILayout.Slider(parameterStateContent, array[i].FloatField, 0, 8);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].LightAction == LightActions.SetRange)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new range value that the light is going to have.");
                array[i].FloatField = EditorGUILayout.FloatField(parameterStateContent, array[i].FloatField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].LightAction == LightActions.SetSpotAngle)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new spot angle value that the light is going to have.");
                array[i].FloatField = EditorGUILayout.Slider(parameterStateContent, array[i].FloatField, 1, 179);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].LightAction == LightActions.SetType)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new light type value that the light is going to have.");
                array[i].LightTypeField = (LightType)EditorGUILayout.EnumPopup(parameterStateContent, array[i].LightTypeField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].LightAction == LightActions.SetColor)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new light color value that the light is going to have.");
                array[i].ColorField = EditorGUILayout.ColorField(parameterStateContent, array[i].ColorField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].LightAction == LightActions.SetEnabled)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new enabled value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }
        else if (array[i].Component == Components.AudioSource)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            GUIContent componentContent = new GUIContent("Component", "The component that is going to do the action.");
            array[i].AudioSourceComponent = (AudioSource)EditorGUILayout.ObjectField(componentContent, array[i].AudioSourceComponent, typeof(AudioSource), true);
            GUIContent actionContent = new GUIContent("Action", "The type of action the component is going to do.");
            array[i].AudioSourceAction = (AudioSourceActions)EditorGUILayout.EnumPopup(actionContent, array[i].AudioSourceAction);
            if (array[i].AudioSourceAction == AudioSourceActions.SetClip)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent audioClipFieldContent = new GUIContent("Value", "The new value for the audio clip.");
                array[i].AudioClipField = (AudioClip)EditorGUILayout.ObjectField(audioClipFieldContent, array[i].AudioClipField, typeof(AudioClip), true);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AudioSourceAction == AudioSourceActions.SetLoop)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new set loop value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AudioSourceAction == AudioSourceActions.SetMute)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new mute value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AudioSourceAction == AudioSourceActions.SetVolume)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new volume value that the audio source is going to have.");
                array[i].FloatField = EditorGUILayout.Slider(parameterStateContent, array[i].FloatField, 0, 1);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].AudioSourceAction == AudioSourceActions.SetEnabled)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new enabled value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }
        else if (array[i].Component == Components.Inventory)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            GUIContent componentContent = new GUIContent("Component", "The component that is going to do the action.");
            array[i].InventoryComponent = (Inventory)EditorGUILayout.ObjectField(componentContent, array[i].InventoryComponent, typeof(Inventory), true);
            GUIContent actionContent = new GUIContent("Action", "The type of action the component is going to do.");
            array[i].InventoryAction = (InventoryActions)EditorGUILayout.EnumPopup(actionContent, array[i].InventoryAction);
            if(array[i].InventoryAction == InventoryActions.AddItem)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent itemFieldContent = new GUIContent("Value", "The data of the item.");
                array[i].ItemField = (Item)EditorGUILayout.ObjectField(itemFieldContent, array[i].ItemField, typeof(Item), true);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if(array[i].InventoryAction == InventoryActions.SetEnabled)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new enabled value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }
        else if(array[i].Component == Components.GameObject)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            GUIContent componentContent = new GUIContent("Component", "The component that is going to do the action.");
            array[i].GameObjectComponent = (GameObject)EditorGUILayout.ObjectField(componentContent, array[i].GameObjectComponent, typeof(GameObject), true);
            GUIContent actionContent = new GUIContent("Action", "The type of action the component is going to do.");
            array[i].GameObjectAction = (GameObjectActions)EditorGUILayout.EnumPopup(actionContent, array[i].GameObjectAction);
            if (array[i].GameObjectAction == GameObjectActions.Destroy)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent destroyDelay = new GUIContent("Delay Value", "The time that is going to pass till the object gets destroyed.");
                array[i].FloatField = EditorGUILayout.FloatField(destroyDelay, array[i].FloatField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if(array[i].GameObjectAction == GameObjectActions.AddComponent)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent componentFieldContent = new GUIContent("Component To Add", "The component that is going to be added.");
                array[i].ComponentField = (AddableComponents)EditorGUILayout.EnumPopup(componentFieldContent, array[i].ComponentField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].GameObjectAction == GameObjectActions.SetActive)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new enabled value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if (array[i].GameObjectAction == GameObjectActions.Instantiate)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent gameObjectFieldContent = new GUIContent("Object To Spawn", "The Prefab of the gameobject we are going to instantiate.");
                array[i].GameObjectField = (GameObject)EditorGUILayout.ObjectField(gameObjectFieldContent, array[i].GameObjectField, typeof(GameObject), true);
                GUIContent vector3FieldContent = new GUIContent("Position To Spawn", "The position where the object is going to spawn.");
                array[i].Vector3Field = EditorGUILayout.Vector3Field(vector3FieldContent, array[i].Vector3Field);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }
        else if(array[i].Component == Components.Camera)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            GUIContent componentContent = new GUIContent("Component", "The component that is going to do the action.");
            array[i].CameraComponent = (Camera)EditorGUILayout.ObjectField(componentContent, array[i].CameraComponent, typeof(Camera), true);
            GUIContent actionContent = new GUIContent("Action", "The type of action the component is going to do.");
            array[i].CameraAction = (CameraActions)EditorGUILayout.EnumPopup(actionContent, array[i].CameraAction);
            if(array[i].CameraAction == CameraActions.SetClearFlags)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent clearFlagsFieldContent = new GUIContent("Value", "The new clear flags value.");
                array[i].ClearFlagsField = (CameraClearFlags)EditorGUILayout.EnumPopup(clearFlagsFieldContent, array[i].ClearFlagsField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if(array[i].CameraAction == CameraActions.SetBackgroundColor)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new color value that the background of the camera is going to have.");
                array[i].ColorField = EditorGUILayout.ColorField(parameterStateContent, array[i].ColorField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if(array[i].CameraAction == CameraActions.SetFieldOfView)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent fieldOfViewContent = new GUIContent("Value", "The new value for the field of view.");
                array[i].FloatField = EditorGUILayout.Slider(fieldOfViewContent, array[i].FloatField, 1, 179);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            else if(array[i].CameraAction == CameraActions.SetEnabled)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                GUIContent parameterStateContent = new GUIContent("Value", "The new enabled value that the component is going to have.");
                array[i].BooleanField = EditorGUILayout.Toggle(parameterStateContent, array[i].BooleanField);
                EditorGUILayout.EndVertical();
                GUILayout.Space(0);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(0);
            EditorGUILayout.EndHorizontal();
        }
    }
}
