using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHD : MonoBehaviour {

	public GameObject Camera; 

	public GameObject DetectorHD;

	public MoveScript CharacterScript;

	public float DetectorRange = 2f;


	void Update()
	{
		RaycastHit hit;
		Ray ParkourLine = new Ray (Camera.transform.position, Vector3.Normalize(DetectorHD.transform.position - Camera.transform.position));

		if(Physics.Raycast(ParkourLine, out hit, DetectorRange))
		{
			if (hit.collider.tag == "Wall") {

				CharacterScript.DetectorHD = true;

			} 

		}else 
		{

			CharacterScript.DetectorHD = false;

		}
	}
}
