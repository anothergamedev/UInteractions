using UnityEngine;

public class CameraController : MonoBehaviour 
{
	/// <summary>
	/// Indicates if the camera is able to move by using inputs.
	/// </summary>
	public bool CanMove = true;
	/// <summary>
	/// Indicates the speed at wich the camera moves in the horizontal axis.
	/// </summary>
	public float HorizontalSpeed = 0.5f;
	/// <summary>
	/// Indicates the speed at wich the camera moves in the vertical axis.
	/// </summary>
	public float VerticalSpeed = 0.5f;

	private void Update()
	{
		if (CanMove) 
		{
			transform.Translate (Vector3.right * Input.GetAxis ("Horizontal") * HorizontalSpeed);
			transform.Translate (Vector3.up * Input.GetAxis ("Vertical") * VerticalSpeed);
		}
	}
}