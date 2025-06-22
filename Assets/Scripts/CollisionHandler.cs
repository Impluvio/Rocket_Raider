using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LvlLoadDelay = 1.5f;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] AudioClip FailureSFX;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem FailureParticles;

    [SerializeField] InputAction previousLevel;
    [SerializeField] InputAction nextLevel;


    int currentScene;
    AudioSource audioSource;
    bool isControllable = true;
    bool isCollidable = true;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("nice");
                break;
            case "Finish":
                Debug.Log("savage");
                RunLevelProgression();
                break;
            default:
                RunCrashSequence();
                break;

        }


    }
    void RunLevelProgression()
    {
        audioSource.Stop();
        isControllable = false;
        audioSource.PlayOneShot(SuccessSFX);
        SuccessParticles.Play(SuccessParticles);
        GetComponent<Locomotion>().enabled = false;
        Invoke("LoadNextLevel", LvlLoadDelay);
    }

    void RunCrashSequence()
    {
        audioSource.Stop();
        isControllable = false;
        audioSource.PlayOneShot(FailureSFX);
        FailureParticles.Play(FailureParticles);
        GetComponent<Locomotion>().enabled = false;
        Invoke("ReloadLevel", LvlLoadDelay);
    }

    void LoadNextLevel()
    {
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    void CycleNextLevel()
    {
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
    void CyclePreviousLevel()
    {
        int previousScene = currentScene - 1;

        if (previousScene < 0)
        {
            previousScene = SceneManager.sceneCountInBuildSettings - 1;
        }

        SceneManager.LoadScene(previousScene);

    }

    void Update()
    {
        if (Keyboard.current.lKey.isPressed)
        {
            Invoke("CyclePreviousLevel", LvlLoadDelay);
        }
        else if (Keyboard.current.nKey.isPressed)
        {
            Invoke("CycleNextLevel", LvlLoadDelay);
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }
}
