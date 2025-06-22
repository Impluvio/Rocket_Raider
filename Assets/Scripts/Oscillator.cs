using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed = 1.5f;
    float movementFactor;


    
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }


    void Update()
    {

        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);








    }
}
