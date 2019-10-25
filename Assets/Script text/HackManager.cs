using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackManager : MonoBehaviour {


	public float AccessMenu = 3;

	public AutoWrite AutoWriteScript; 

	public string[] Répliques = {"Joueur1","Ordi1","Joueur2","Ordi2"};


	//true = Peut écrire ; false = Ordinateur


	public int ArrayManagerNumber = 0;




	void Start () {

		ArrayManagerNumber = 1;
		AutoWriteScript.WriteStart();

	}
	



	void Update () {

		if (ArrayManagerNumber == 1) {


				AutoWriteScript.réplique1 ();


		}


		if (ArrayManagerNumber == 2) {


			AutoWriteScript.réplique2 ();


		}


		if (ArrayManagerNumber == 3) {


			AutoWriteScript.réplique3 ();


		}


		if (ArrayManagerNumber == 4) {


			AutoWriteScript.réplique4 ();


		}

		if (ArrayManagerNumber == Répliques.Length) {

			AccessMenu -= Time.deltaTime;

			if (AccessMenu <= 0){

				AccessMenu = 0;

                SceneManager.LoadScene(1);
            }
	}

	}

}
