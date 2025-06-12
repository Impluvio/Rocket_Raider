using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
       

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("nice");
                break;
            case "Finish":
                Debug.Log("savage");
                break;
            default:
                SceneManager.LoadScene(0);
                break;


        }
    }
}
