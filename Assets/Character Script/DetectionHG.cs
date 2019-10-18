using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHG : MonoBehaviour {

	public GameObject Camera; 

	public GameObject DetectorHG;

	public MoveScript CharacterScript;

	public float DetectorRange = 2f;


	void Update()
	{
		RaycastHit hit;
		Ray ParkourLine = new Ray (Camera.transform.position, Vector3.Normalize(DetectorHG.transform.position - Camera.transform.position));

		if(Physics.Raycast(ParkourLine, out hit, DetectorRange))
		{
			if (hit.collider.tag == "Wall") {

				CharacterScript.DetectorHG = true;

			} 

		}else 
		{

			CharacterScript.DetectorHG = false;

		}
	}
}

