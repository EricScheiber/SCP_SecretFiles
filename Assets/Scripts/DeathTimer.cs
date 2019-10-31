using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float DeathTime;

    private CharacterController controller;
    public float currentTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentTime = DeathTime;
    }

    void Update()
    {
        if (controller.velocity.magnitude == 0f)
        {
            currentTime -= Time.deltaTime;
        }
    }
}
