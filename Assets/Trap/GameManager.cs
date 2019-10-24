using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public float LastTrig;

    public float LastTrigBase = 10f;


    public bool TriggersActive;

    public bool Timerlast = false;



	void Start () {


    }
	
	
	void Update () {

        if (Timerlast == true)
        {

            LastTrig = -Time.deltaTime;
            TriggersActive = false;
        }
		
        if (LastTrig <= 0f)
        {

            Timerlast = false;
            TriggersActive = true;
            LastTrig = LastTrigBase;
            

        }

	}
}
