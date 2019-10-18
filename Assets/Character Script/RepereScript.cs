using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepereScript : MonoBehaviour {

	public float Recup = 1f;

	public bool RecupTimeRepere;

	public GameObject Character;

	public ClimbScript Climb; 



	void Start () 
	{

		RecupTimeRepere = true;

		Character = GameObject.Find ("Character");

		Climb = Character.GetComponent<ClimbScript> ();

		Debug.Log ("Character Get");
	}
	


	void Update () 
	{

		if (RecupTimeRepere == true){
			Debug.Log ("Timer on");
			Recup -= Time.deltaTime;

			if (Recup <= 0) 
			{
				Debug.Log ("Téléportation !");
				Character.transform.position = gameObject.transform.position;
				Climb.CanClimb = false;
				AutoDestruction ();
			}

		}

	}




	void AutoDestruction ()
	{
		Debug.Log ("Repere is dead");
		Climb.RepereLimit = false;
		Destroy (gameObject);

	}


}
