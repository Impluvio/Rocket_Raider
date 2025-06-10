using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Locomotion : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 4f;
    [SerializeField] InputAction rotation;

    Rigidbody rb;


    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }


    private void FixedUpdate()
    {
        ApplyThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        
    }

    private void ApplyThrust()
    {
        if (thrust.IsPressed())
        {
            // Vector3 newForce = new Vector3(0,10,0);
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }



}
