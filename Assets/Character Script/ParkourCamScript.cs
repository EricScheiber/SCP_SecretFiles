using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourCamScript : MonoBehaviour {

	public Animator Climbing;
	Vector3 OriginalPos;

	void Start(){
		
		Climbing = GetComponent<Animator> ();

	}

	public void ClimbingView() {
		OriginalPos = transform.localPosition;
		Climbing.enabled = true;
	

	}

	public void ClimbingEnd()
	{
		

		Climbing.SetTrigger("Anim_End");
		Climbing.enabled = false;
		this.enabled = false;


	}
	

}
