using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Platform : MonoBehaviour
{

    private Vector3 _startPosition;
    private float[] randomDifferenceArray;
    private Vector3[] startPositionArray;
    private int children;
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 MovementVector = new Vector3(0f,0f,5f);
    


    // Start is called before the first frame update
    void Start()
    {
 

        // Count all the children in the parent gameObject
        children = transform.childCount;
       
        
        // Instantiate both arrays
        randomDifferenceArray = new float[children];
        startPositionArray = new Vector3[children];


        for (int i = 0; i < children; ++i)
        {
            startPositionArray[i] = transform.GetChild(i).position;
            randomDifferenceArray[i] = Random.Range(0.0f, 4.0f);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (period >= Mathf.Epsilon)
        {
            float cycles = Time.time / period;

            const float tau = Mathf.PI * 2; // around 6.28



            for (int i = 0; i < children; ++i)
            {

                float rawSin = Mathf.Sin(cycles + randomDifferenceArray[i] * tau);
                Vector3 offset = rawSin * MovementVector;

                //transform.GetChild(i).position = startPositionArray[i] + new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time + randomDifferenceArray[i]) * multiplier);
                transform.GetChild(i).position = startPositionArray[i] + offset;

            }
        }
        

       
    }
}
