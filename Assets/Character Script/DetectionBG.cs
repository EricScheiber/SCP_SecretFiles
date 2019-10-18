using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBG : MonoBehaviour {

	public GameObject Camera; 

	public GameObject DetectorBG;

	public MoveScript CharacterScript;

	public float DetectorRange = 2f;


	void Update()
	{
		RaycastHit hit;
		Ray ParkourLine = new Ray (Camera.transform.position, Vector3.Normalize(DetectorBG.transform.position - Camera.transform.position));

		if(Physics.Raycast(ParkourLine, out hit, DetectorRange))
		{
			if (hit.collider.tag == "Wall") {

				CharacterScript.DetectorBG = true;

			} 

		}else 
		{

			CharacterScript.DetectorBG = false;

		}
	}
}
