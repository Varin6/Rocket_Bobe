using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private Vector3 _startPosition;
    private float[] randomDifferenceArray;
    private Vector3[] startPositionArray;
    private int children;

    
    [SerializeField] float multiplier;


    // Start is called before the first frame update
    void Start()
    {
 

        // Count all the children in the parent gameObject
        children = transform.childCount;
       
        // set default value for multiplier
        multiplier = 5;

        // Instantiate both arrays
        randomDifferenceArray = new float[children];
        startPositionArray = new Vector3[children];


        // Loop over all the children and set a startPosition and Randomdifference
        // for each of them. this will be used in Update method.

        for (int i = 0; i < children; ++i)
        {
            startPositionArray[i] = transform.GetChild(i).position;
            randomDifferenceArray[i] = Random.Range(0.0f, 4.0f);
            
        }
            

        
    }

    // Update is called once per frame
    void Update()
    {


        for (int i = 0; i < children; ++i)
        {

            print(Time.time);

            transform.GetChild(i).position = startPositionArray[i] + new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time + randomDifferenceArray[i]) * multiplier);

        }

       
    }
}
