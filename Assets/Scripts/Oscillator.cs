using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPostiton;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;


    void Start()
    {
        startingPostiton = transform.position;
    }

    void Update()
    {
        if(period == 0)
        {
            Debug.Log("Period can't be 0");
            period = 1f;
        }
        ChangePostion();
    }

    void ChangePostion()
    {
        movementFactor = (RawSinWave() + 1f) / 2f; //recalculation to go from 0 to 1

        Vector3 offset = movementFactor * movementVector;

        transform.position = offset + startingPostiton;
    }

    // returns float from -1 to 1
    float RawSinWave() 
    {
        float cycles = Time.time / period; //continually growing over time

        const float tau = Mathf.PI * 2; 

        return Mathf.Sin(cycles * tau);
    }
}
