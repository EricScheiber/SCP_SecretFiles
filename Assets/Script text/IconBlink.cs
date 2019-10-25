using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconBlink : MonoBehaviour {
 

	float IconX, IconY;

	public float TimeBlink = 0.9f;

	public bool TypingMode;

	public bool Blinking;

	public bool CanWrite = false;

	public GameObject Icon;

	public Vector3 IconPosition;

	public Vector3 OldIconPosition;

	public float NewIconPositionX;

	public float NewIconPositionY;

	public float NewIconPositionZ;

	public float SpaceMove = 5.05f;

	public Vector3 StartPosition;

	public bool NewSentence = false;

	public bool ActiveBlink = false;

	public float LineComputer = 0;




	void Start (){

		StartPosition.x = Icon.transform.position.x; 
		StartPosition.y = Icon.transform.position.y;  

		OldIconPosition.x = StartPosition.x;
		OldIconPosition.y = StartPosition.y; 

		StartPosition.x = OldIconPosition.x;
		StartPosition.y = OldIconPosition.y;

		ActiveBlink = true; 

	}
		


	void Update () {

		if (NewSentence == true) {

			Icon.transform.position = new Vector3 (StartPosition.x, StartPosition.y -= LineComputer, Icon.transform.position.z);
	
			OldIconPosition.x = StartPosition.x;
			OldIconPosition.y = StartPosition.y;

			NewSentence = false;
		}

		if (CanWrite == true){
			
		TimeBlink -= Time.deltaTime;



		if ((TimeBlink < 0) && (TypingMode == true)) {
		
			Blinking = !Blinking;
			TimeBlink = 0.65f;
		}
		if ((TimeBlink < 0) && (TypingMode == false)) {

			Blinking = !Blinking;
			TimeBlink = 0.65f;
		}
				
	  }
		if (Blinking == true && ActiveBlink == true) {
			Icon.SetActive (true);
		}

		if (Blinking == false && ActiveBlink == true) {
			Icon.SetActive (false);
		}
	}

	public void MoveIcon (){

		NewIconPositionX = OldIconPosition.x + SpaceMove;
		NewIconPositionY = OldIconPosition.y;

		Icon.transform.position = new Vector3 (NewIconPositionX, NewIconPositionY, IconPosition.z); 

		OldIconPosition.x = Icon.transform.position.x;
		OldIconPosition.y = Icon.transform.position.y;

	}
		



}
