using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

	[Header("Speed List")]

	public float speed;

	public float WalkSpeed = 6.0f;

	public float CrouchSpeed = 3.0f;

	public float SprintSpeed = 12.0f;

	public float SlideSpeed;

	public float RunUp = 10.0f;


	[Header("Vertical forces")]


	public float gravity = 14.0f;

	public float verticalVelocity;

	public float Jumpforce = 5.0f;

	public float RotationSlideX;

	public float DetectorsON;



	[Header("Reference script")]



	public CameraScript Camera;

	public SlideScript SlideModeScript;

	public Rigidbody rb;

	public CharacterController CharCon;

	public CameraPositionScript CamPos;

	public EscaladeScript EscaladeMode; 

	public WallRunScript WallRun;

	public ClimbScript Climbing;



	[Header("Detection and mode")]


	public bool ParkourContact;

	public bool RecupTime = false;

	public bool OnFloor;

	public bool WalkMode;

	public bool CrouchMode;

	public bool SprintMode = false;

	public bool MoveMode;

	public bool CanSlide = false;


	[Header("Detectors")]


	public bool DetectorHD;

	public bool DetectorHG;

	public bool DetectorBD;

	public bool DetectorBG;




	void Start () 
	{

		MoveMode = true;

		WalkMode = true;

		CharCon = GetComponent<CharacterController>();

		rb = GetComponent<Rigidbody>();

	}

	void Update()
	{

		// Boite de vitesse


		if (WalkMode && MoveMode)
			speed = WalkSpeed;

		if ((CrouchMode == true) && (MoveMode == true))
			speed = CrouchSpeed;

		if ((SprintMode == true) && (MoveMode == true))
			speed = SprintSpeed;



		if ((Input.GetKey(KeyCode.C)) && (MoveMode == true)) 
		{
			
			WalkMode = false;
			CrouchMode = true;


		}


		if (CrouchMode == true) {

			transform.localScale = new Vector3 (1, 0.5f, 1);

		} 

		if ((CrouchMode == true) && (CanSlide == false)) {

			speed = CrouchSpeed;

		}

		if (CrouchMode == false) {
	
			transform.localScale = new Vector3 (1, 1, 1);

		}
			// Sprinter

		if ((Input.GetKey (KeyCode.LeftShift)) && (CrouchMode == false) && (OnFloor == true)) 
		    {
				WalkMode = false;
				SprintMode = true;
				speed = SprintSpeed;
				CanSlide = true;
			} 

			if (Input.GetKeyUp (KeyCode.LeftShift) && (SprintMode == true)) 
		    {
				SprintMode = false;
				WalkMode = true;
				CanSlide = false;
			} 


		// Escalade

		if ((DetectorHG == true) && (DetectorHD == true) && (DetectorBG == true) && (DetectorBD == true) && (SprintMode == true) && (OnFloor == true)) {

			EscaladeMode.EscaladeMode ();

		}


		// Wallrun Droite
			
		if ((DetectorHG == false) && (DetectorHD == true) && (DetectorBG == false) && (DetectorBD == true) && (SprintMode == true) && (OnFloor == true)) {

			WallRun.WallRunDroite ();

		}


		// Wallrun Gauche

		if ((DetectorHG == true) && (DetectorHD == false) && (DetectorBG == true) && (DetectorBD == false) && (SprintMode == true) && (OnFloor == true)) {

			WallRun.WallRunGauche ();

		}

		//Climb


		if ((DetectorHG == false) && (DetectorHD == false) && (DetectorBG == true) && (DetectorBD == true) && (OnFloor == false)) {

			Climbing.CanClimbDetection = true;

		} else {

			Climbing.CanClimbDetection = false;

		}


	}

	void FixedUpdate () 
	{


		if ((Input.GetKeyUp(KeyCode.C))&& (MoveMode == true)) {


			WalkMode = true;
			CrouchMode = false;


		}

		if ((Input.GetKeyDown(KeyCode.C))&& (MoveMode == true)) {

			CamPos.CrouchDown ();
		}
			

		// Suis-je au sol ?


		if (CharCon.isGrounded) {
			
			OnFloor = true;
			SlideModeScript.OnFloor = true;
			verticalVelocity = -gravity * Time.deltaTime;

		} else {

			OnFloor = false;

		}




		// Saut

		if (Input.GetKeyDown(KeyCode.Space) && (OnFloor == true) && (CrouchMode == false)) {

			verticalVelocity = Jumpforce;

		} else if (OnFloor == false){

			verticalVelocity -= gravity * Time.deltaTime;

		}




		// Déplacement
	
		if (MoveMode == true) 
		{
			
			float deltaX = Input.GetAxis ("Horizontal") * speed;
			float deltaZ = Input.GetAxis ("Vertical") * speed;

			Vector3 movement = new Vector3 (deltaX, verticalVelocity, deltaZ);
			movement = Vector3.ClampMagnitude (movement, speed);

			movement *= Time.deltaTime;
			movement = transform.TransformDirection (movement);
			CharCon.Move (movement);
		}



		// Glisser

		if ((CanSlide == true) && (Input.GetKeyDown(KeyCode.C))) 
		{
			
			CrouchMode = true;
			transform.localScale = new Vector3 (1, 0.5f, 1);
			CamPos.CrouchDown ();
			SlideModeScript.SlideMode ();
		}
			
			
	}



		
}
