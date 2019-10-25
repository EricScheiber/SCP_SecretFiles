using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour 
{


	// Use this for initialization
	public enum RotationAxis{
		MouseX = 1,
		MouseY = 2
	} 

	public RotationAxis axes = RotationAxis.MouseX;

	public float MinVert = -75.0f;
	public float MaxVert = 75.0f;

	public float Sensibility = 10.0f;
	public float sensHorizontal;
	public float sensVertical;

	public float RotationX;

	public float CrouchHeight = 1.0f;


	public bool CameraSwitch;


	void Start ()
	{

		CameraSwitch = true;

		sensVertical = Sensibility;

		sensHorizontal = Sensibility;

		Cursor.lockState = CursorLockMode.Locked;

	}




	public void Update () {



		if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None;



		if (axes == RotationAxis.MouseX) {

			transform.Rotate (0, Input.GetAxis ("Mouse X") * sensHorizontal, 0);

		} else if (axes == RotationAxis.MouseY) {

			RotationX -= Input.GetAxis ("Mouse Y") * sensVertical;
			RotationX = Mathf.Clamp (RotationX, MinVert, MaxVert);

			float RotationY = transform.localEulerAngles.y;

			transform.localEulerAngles = new Vector3 (RotationX, RotationY, 0);


		}


	}


		


}
