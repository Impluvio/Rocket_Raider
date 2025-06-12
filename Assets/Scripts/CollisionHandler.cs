using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LvlLoadDelay = 1.5f;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] AudioClip FailureSFX;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem FailureParticles; 

    int currentScene;
    AudioSource audioSource;
    bool isControllable = true;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) { return; }

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


}
