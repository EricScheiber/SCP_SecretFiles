using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourWallScript : MonoBehaviour {

	public GameObject Camera; 

	public GameObject Detector;

	public MoveScript CharacterScript;

	public float DetectorRange = 2f;


	void Update()
	{
		RaycastHit hit;
		Ray ParkourLine = new Ray (Camera.transform.position, Vector3.Normalize(Detector.transform.position - Camera.transform.position));
	
		if(Physics.Raycast(ParkourLine, out hit, DetectorRange))
		{
			if (hit.collider.tag == "Wall") {
				
				CharacterScript.ParkourContact = true;

			} 

		}else 
		{

			CharacterScript.ParkourContact = false;

		}
	}
}
