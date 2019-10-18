using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour {


    public bool ActiveTrigger = true;
    public bool DurationActive = false;


    public int ProjectType;

    public float Duration;
    public float DurationBase = 7f;


    public GameObject ParticleDown;

    public GameObject ParticleUp;

    public GameObject ParticleAll;


    public GameManager GM;


    private void Start()
    {
        Duration = DurationBase;
    }

    void Update()
    {
       
        if (DurationActive == true)
        {
            Duration = -Time.deltaTime;
        }

        if (Duration <= 0)
        {

            DurationActive = false;
            Duration = DurationBase;

            ParticleUp.SetActive(false);
            ParticleDown.SetActive(false);
            ParticleAll.SetActive(false);
        }

        if (GM.TriggersActive == true)
        {

            ActiveTrigger = true;

        }
     

    }


    void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "Player") && (ActiveTrigger == true))
        {

            ProjectType = Random.Range(0, 3);

            Projection();


        }
        

    }


    void Projection()
    {


        if (ProjectType == 1)
        {

            ParticleUp.SetActive(true);
            GM.Timerlast = true;

        }

        if (ProjectType == 2)
        {

            ParticleDown.SetActive(true);
            GM.Timerlast = true;

        }

        if (ProjectType == 3)
        {

            ParticleAll.SetActive(true);
            GM.Timerlast = true;

        }

    }

}
