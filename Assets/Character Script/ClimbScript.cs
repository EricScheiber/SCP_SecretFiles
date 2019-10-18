using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbScript : MonoBehaviour {

	public Vector3 ClimbPlace;

	public GameObject Repere;

	public MoveScript Move;

	public EscaladeScript Escalade;

	public WallRunScript WallRun;

	public Camera MainCam;

	public Camera ParkourCam;

	public ParkourCamScript ClimbCam;

	public bool CanClimb;

	public bool CanClimbDetection;

	public bool RepereLimit = false;

	void Start () {
		
	}
	

	void Update () {

		if ((CanClimb == true) && (CanClimbDetection == true)) {

			ClimbPlace = GameObject.Find("ClimbPlace").transform.position;
			Move.enabled = false;
			Escalade.enabled = false;
			WallRun.enabled = false;
		    MainCam.enabled = false;
			ParkourCam.enabled = true;
			ClimbCam.ClimbingView ();
			Escalade.Recup = 0;
			WallRun.Recup = 0;


			if (RepereLimit == false) {
				Debug.Log ("Repere instantiated");
				Instantiate (Repere, ClimbPlace, Quaternion.identity);

				RepereLimit = true;
			}

		} else {
			
			Move.enabled = true;
			Escalade.enabled = true;
			WallRun.enabled = true;
			MainCam.enabled = true;
			ParkourCam.GetComponent<ParkourCamScript> ().ClimbingEnd ();
			//ParkourCam.enabled = false;


		}

	}


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "ClimbCorner") 
		{
			Debug.Log ("Climbing");
			CanClimb = true;
		}
	}

}
