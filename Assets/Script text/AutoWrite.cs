using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AutoWrite : MonoBehaviour {





	public string[] Letters_2 =  { "a",  "b",  "c",  "d",  "e",  "f",  "g",  "h",  "i", "j",  "k",  "l",  "m",  "n",  "o",  "p",  "q",  "r",  "s",  "t",  "u",  "v",  "w",  "x",  "y",  "z", " "};

	//public float[] IconMovementOnX = { 5.05f, 5.05f,5.08f,5.05f,5.9f,5.05f,5.0f,4.6f,2.00f,2.00f,5.05f,2.5f,5.05f,5.05f,5.05f,5.05f,5.05f,5.05f,5.0f,5.05f,5.05f,5.05f,5.05f,5.05f,5.05f,5.0f,1.6f};

	public float[] IconMovement = { 5.05f, 5.05f,5.08f,5.05f,5.9f,5.05f,5.0f,4.6f,2.00f,2.00f,5.05f,2.5f,5.05f,5.05f,5.05f,5.05f,5.05f,5.05f,5.0f,5.05f,5.0f,5.05f,5.05f,5.05f,5.05f,5.0f,1.6f};

	public int[] StoryTell_Réplique1 = {7, 0, 2, 10, 26, 18, 2, 15 };

	public int[] StoryTell_Réplique3 = {0,2,2,4,18,18,26,5,8,11,4,26,0,6,4,13,19,26,5,14,23 };

	public int[] StoryTell_LastRéplique = {0 ,2 ,2 ,4 ,18 ,18 ,26 ,6 ,17 ,0 ,13 ,19 ,4 ,3 };


		/*
	 a = 0
	 b = 1
	 c = 2
	 d = 3
	 e = 4
	 f = 5
	 g = 6
	 h = 7
	 i = 8
	 j = 9
	 k = 10
	 l = 11
	 m = 12
	 n = 13
	 o = 14
	 p = 15
	 q = 16
	 r = 17
	 s = 18
	 t = 19
	 u = 20
	 v = 21
	 w = 22
	 x = 23
	 y = 24
	 z = 25
	 espace = 26
	 */


	//Tableau Hacker



	public IconBlink GoMoveIcon;

	public int ArrayNumber = 0;

	private bool Sentence_Writing = false;


	public int NextLetter = 0;

	public string NewLetter;







	public Text OldText;  

	public Text Hacker;

	public HackManager HackManagerScript;

	public int Loadingdots = 0;

	public bool Loading = false;

	public float Loadingtime = 1.0f;

	public bool ComputerLoadText;

	public int StorytellPlace = 0;





	void Update(){
		Debug.Log (NewLetter);
		Debug.Log (NextLetter);
	}




	public void WriteStart(){

		NextLetter= StoryTell_Réplique1[0] ;   //Nextletter = 8

		NewLetter = Letters_2[NextLetter]; // Newletter = lettre n°8 lettre du tableau letter

		GoMoveIcon.SpaceMove =  IconMovement [NextLetter];


	}

	public void WriteStart2(){

		StorytellPlace = 0;

		GoMoveIcon.LineComputer = 38.0f;

		NextLetter= StoryTell_LastRéplique[0] ;   //Nextletter = 0

		NewLetter = Letters_2[NextLetter]; // Newletter = lettre n°0 lettre du tableau letter

		GoMoveIcon.SpaceMove =  IconMovement [NextLetter];

		OldText.text = Hacker.text;
		Hacker.text = OldText.text + "\n\n";


	}


	public void réplique1 ()
	{

		Sentence_Writing = true;
		GoMoveIcon.CanWrite = true;
	


		if ((Input.anyKeyDown) && (Sentence_Writing == true)) {

			OldText.text = Hacker.text;

			GoMoveIcon.MoveIcon();

			Hacker.text = OldText.text + NewLetter;

			StorytellPlace += 1;


			if (StorytellPlace < StoryTell_Réplique1.Length) {

				NextLetter = StoryTell_Réplique1 [StorytellPlace];

				NewLetter = Letters_2 [NextLetter];

				GoMoveIcon.SpaceMove = IconMovement [NextLetter];


			}

			else if (StorytellPlace  >= StoryTell_Réplique1.Length) {

				HackManagerScript.ArrayManagerNumber += 1;




				ComputerLoadText = true;
				GoMoveIcon.CanWrite = false;
				GoMoveIcon.ActiveBlink = false;
				GoMoveIcon.Blinking = false;
				GoMoveIcon.Icon.SetActive (false);
			}


		}
			

	}




	public void réplique2 () {


		Sentence_Writing = false;

		GoMoveIcon.CanWrite = true;
		GoMoveIcon.Blinking = true;





		if (ComputerLoadText == true) {
			OldText.text = Hacker.text;
			Hacker.text = OldText.text + "\n\nHacking in progress.";
			ComputerLoadText = false;
			Loading = true;

		}

		if ((Loading == true) && (Loadingdots < 3)) {

			Loadingtime -= Time.deltaTime;

			if (Loadingtime < 0) {
				OldText.text = Hacker.text;
				Hacker.text = OldText.text + ".";
				Loadingdots += 1;
				Loadingtime = 1.0f;

			}

		}

		if (Loadingdots == 3) {

			Loading = false;
			OldText.text = Hacker.text;
			Hacker.text = OldText.text + "Done";
			Loadingdots = 0;
			WriteStart2();
			HackManagerScript.ArrayManagerNumber += 1;
			GoMoveIcon.NewSentence = true;
		}	


	}


	public void réplique3 () {

		Sentence_Writing = true;

		GoMoveIcon.ActiveBlink = true;

	

			if ((Input.anyKeyDown) && (Sentence_Writing == true)) {

				OldText.text = Hacker.text;

				Hacker.text = OldText.text + NewLetter;

			    GoMoveIcon.MoveIcon ();

				StorytellPlace += 1;

			if (StorytellPlace >= StoryTell_Réplique3.Length) {

				Debug.Log ("fin de texte");

				ComputerLoadText = true;
				GoMoveIcon.CanWrite = false;
				GoMoveIcon.ActiveBlink = false;
				GoMoveIcon.Blinking = false;
				GoMoveIcon.Icon.SetActive (false);

				HackManagerScript.ArrayManagerNumber += 1;

				StorytellPlace = 0;



			} else if (StorytellPlace <= StoryTell_Réplique3.Length) {

				NextLetter = StoryTell_Réplique3 [StorytellPlace];

				NewLetter = Letters_2 [NextLetter];

				GoMoveIcon.SpaceMove = IconMovement [NextLetter];

		
				}


			}
		}

	public void réplique4 () {


		Sentence_Writing = false;

		GoMoveIcon.CanWrite = true;
		GoMoveIcon.Blinking = true;





		if (ComputerLoadText == true) {
			OldText.text = Hacker.text;
			Hacker.text = OldText.text + "\n\nLoading.";
			ComputerLoadText = false;
			Loading = true;

		}

		if ((Loading == true) && (Loadingdots < 3)) {

			Loadingtime -= Time.deltaTime;

			if (Loadingtime < 0) {
				OldText.text = Hacker.text;
				Hacker.text = OldText.text + ".";
				Loadingdots += 1;
				Loadingtime = 1.0f;

			}

		}

		if (Loadingdots == 3) {

			Loading = false;
			OldText.text = Hacker.text;
			Hacker.text = OldText.text + "Access Granted Level 4";
			WriteStart2();
			HackManagerScript.ArrayManagerNumber += 1;
			GoMoveIcon.NewSentence = true;
		}	


	}





	}



	

	





