using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideScript : MonoBehaviour {

	public bool RecupTime = false;

	public float RecupBase = 5.0f;

	public float Recup;

	public float RotationSlideX;

	public float SlideSpeed = 12f;




	public GameObject Character;

	public CameraScript Camera;

	public CameraPositionScript CamPosChange;

	public CharacterController CharCon;

	public MoveScript MoveCharaScript;


	public bool SlidePush = false;

	public bool OnFloor;



	void Start(){

		Recup = RecupBase;

	}



	void Update(){

	if (RecupTime == true)
	{

		Recup -= Time.deltaTime;

		if (Recup <= 0) 
		{
			RecupTime = false;
			Recup = RecupBase;
			SlidePush = false;
			MoveCharaScript.CrouchMode = false;
			Camera.enabled = true;
			MoveCharaScript.MoveMode = true;
			Character.GetComponent<CameraScript>().enabled = true;
		}

	}	

		if (SlidePush == true) 
		{

			CharCon.Move(transform.forward * 8f * Time.deltaTime);
			MoveCharaScript.verticalVelocity -= MoveCharaScript.gravity * Time.deltaTime;
		}
}
		
	public void SlideMode() 
	{


		RecupTime = true;
		Camera.enabled = false;
		MoveCharaScript.MoveMode = false;
		Character.GetComponent<CameraScript>().enabled = false;
		SlidePush = true;

	}

}
