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
    [SerializeField] float rotationSpeed = 0.5f;

    [SerializeField] AudioClip MainEngine;
    [SerializeField] ParticleSystem MainThruster;
    [SerializeField] ParticleSystem LeftSideThruster;
    [SerializeField] ParticleSystem RightSideThruster;

    Rigidbody rb;
    AudioSource audioSource;  


    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        float adjustedRotationSpeed = rotationSpeed * Time.fixedDeltaTime;
        RightSideThruster.Stop();
        LeftSideThruster.Stop();

        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateLeft(adjustedRotationSpeed);
        }
        else if (rotationInput > 0)
        {
            RotateRight(adjustedRotationSpeed);
        }


    }

    private void RotateRight(float adjustedRotationSpeed)
    {
        RightSideThruster.Play();
        ApplyRotation(-adjustedRotationSpeed);
    }

    private void RotateLeft(float adjustedRotationSpeed)
    {
        ApplyRotation(adjustedRotationSpeed);
        LeftSideThruster.Play();
    }

    private void ApplyRotation(float rotationPerFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, rotationPerFrame);
        rb.freezeRotation = false;
    }

    private void ApplyThrust()
    {
        if (thrust.IsPressed())
        {
            // Vector3 newForce = new Vector3(0,10,0);
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            MainThruster.Play();
            playAudio();
        }
        else
        {
            StopRotation();
        }


    }

    private void StopRotation()
    {
        MainThruster.Stop();
        audioSource.Stop();
    }

    private void playAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine);
        }

        

    }
}
