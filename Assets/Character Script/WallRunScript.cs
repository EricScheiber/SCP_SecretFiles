using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunScript: MonoBehaviour {

	public float Recup;

	public float RecupBase = 2.5f;

	public bool RecupTimeWallRun;

	public bool PushWallRunGauche = false;

	public bool PushWallRunDroite = false;


	public float WallRunSpeed = 6f;


	public GameObject Character;

	public CameraScript MyCamera;

	public CameraPositionScript CamPosChange;

	public CharacterController CharCon;

	public MoveScript MoveCharaScript;

	public CameraPositionScript CameraPosition;


	void Start () {
		Recup = RecupBase;
	}

	void Update () {

		if (RecupTimeWallRun == true)
		{

			Recup -= Time.deltaTime;

			if (Recup <= 0) 
			{
				RecupTimeWallRun = false;
				MoveCharaScript.gravity = 14f;
				Recup = RecupBase;
				MyCamera.enabled = true;
				MoveCharaScript.MoveMode = true;
				Character.GetComponent<CameraScript>().enabled = true;
			}

			if (PushWallRunGauche == true || PushWallRunDroite == true)
			{

				CharCon.Move(transform.forward * WallRunSpeed * Time.deltaTime);
				MoveCharaScript.verticalVelocity -= MoveCharaScript.gravity * Time.deltaTime;
			}



		}	

	}


	public void WallRunDroite () {
		RecupTimeWallRun = true;
		MoveCharaScript.gravity = 0f;
		MyCamera.enabled = false;
		MoveCharaScript.MoveMode = false;
		Character.GetComponent<CameraScript>().enabled = false;
		PushWallRunDroite = true;
	}

	public void WallRunGauche () {
		RecupTimeWallRun = true;
		MoveCharaScript.gravity = 0f;
		MyCamera.enabled = false;
		MoveCharaScript.MoveMode = false;
		Character.GetComponent<CameraScript>().enabled = false;
		PushWallRunGauche = true;
	}

}
