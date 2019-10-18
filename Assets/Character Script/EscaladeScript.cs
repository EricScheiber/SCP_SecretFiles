using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaladeScript : MonoBehaviour {

	public float Recup;

	public float RecupBase = 1.0f;

	public bool RecupTimeEscalade;

	public bool ElevationPush = false;


	public GameObject Character;

	public CameraScript Camera;

	public CameraPositionScript CamPosChange;

	public CharacterController CharCon;

	public MoveScript MoveCharaScript;

	public CameraPositionScript CameraPosition;



	// Use this for initialization
	void Start () {
		Recup = RecupBase;
	}
	
	// Update is called once per frame
	void Update () {

		if (RecupTimeEscalade == true)
		{

			Recup -= Time.deltaTime;

			if (Recup <= 0) 
			{
				RecupTimeEscalade = false;
				Recup = RecupBase;
				Camera.enabled = true;
				MoveCharaScript.MoveMode = true;
				Character.GetComponent<CameraScript>().enabled = true;
			}

			if (ElevationPush == true) 
			{

				CharCon.Move(transform.up * 2f * Time.deltaTime);
				MoveCharaScript.verticalVelocity -= MoveCharaScript.gravity * Time.deltaTime;
			}



		}	

	}


	public void EscaladeMode () {
	
		RecupTimeEscalade = true;
		Camera.enabled = false;
		MoveCharaScript.MoveMode = false;
		Character.GetComponent<CameraScript>().enabled = false;
		ElevationPush = true;

	}


}
