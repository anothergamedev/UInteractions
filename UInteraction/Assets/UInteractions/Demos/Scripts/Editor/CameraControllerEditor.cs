using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor 
{
	public override void OnInspectorGUI ()
	{
		CameraController myTarget = (CameraController)target;

		GUIContent canMoveContent = new GUIContent ("Can Move", "Indicates if the camera is able to move by using inputs.");
		myTarget.CanMove = EditorGUILayout.Toggle (canMoveContent, myTarget.CanMove);
		if (myTarget.CanMove) 
		{
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			EditorGUILayout.BeginVertical ();
			GUIContent horizontalSpeedContent = new GUIContent ("Horizontal Speed", "Indicates the speed at wich the camera moves in the horizontal axis.");
			myTarget.HorizontalSpeed = EditorGUILayout.FloatField (horizontalSpeedContent, myTarget.HorizontalSpeed);
			GUIContent verticalSpeedContent = new GUIContent ("Vertical Speed", "Indicates the speed at wich the camera moves in the vertical axis.");
			myTarget.VerticalSpeed = EditorGUILayout.FloatField (verticalSpeedContent, myTarget.VerticalSpeed);
			EditorGUILayout.EndVertical ();
			GUILayout.Space (0);
			EditorGUILayout.EndHorizontal ();
		}
	}
}
