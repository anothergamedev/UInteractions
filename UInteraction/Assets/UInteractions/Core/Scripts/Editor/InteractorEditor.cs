using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactor))]
[CanEditMultipleObjects]
public class InteractorEditor : Editor 
{
	public override void OnInspectorGUI ()
	{
		Interactor myTarget = (Interactor)target;

		GUIContent canInteractContent = new GUIContent ("Can Interact", "Indicates if the player is able to interact with objects.");
		myTarget.CanInteract = EditorGUILayout.Toggle (canInteractContent, myTarget.CanInteract);
		if (myTarget.CanInteract) 
		{
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			EditorGUILayout.BeginVertical ();
			GUIContent keyContent = new GUIContent ("KeyCode", "Key we are going to use to interact with other objects.");
			myTarget.Key = (KeyCode)EditorGUILayout.EnumPopup (keyContent, myTarget.Key);
			GUIContent rangeContent = new GUIContent ("Range", "The range at wich we can interact with other objects.");
			myTarget.Range = EditorGUILayout.FloatField (rangeContent, myTarget.Range);
			EditorGUILayout.EndVertical ();
			GUILayout.Space (0);
			EditorGUILayout.EndHorizontal ();
		}
	}
}
