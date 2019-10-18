using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBD : MonoBehaviour {

	public GameObject Camera; 

	public GameObject DetectorBD;

	public MoveScript CharacterScript;

	public float DetectorRange = 2f;


	void Update()
	{
		RaycastHit hit;
		Ray ParkourLine = new Ray (Camera.transform.position, Vector3.Normalize(DetectorBD.transform.position - Camera.transform.position));

		if(Physics.Raycast(ParkourLine, out hit, DetectorRange))
		{
			if (hit.collider.tag == "Wall") {

				CharacterScript.DetectorBD = true;

			} 

		}else 
		{

			CharacterScript.DetectorBD = false;

		}
	}
}

