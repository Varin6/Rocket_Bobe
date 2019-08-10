using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform rocket;
    

    public float distanceFromObject = 10f;

    // Start is called before the first frame update
    void Start()
    {

        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();

       
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 playerLastPosition = transform.position + rocket.position;

        Vector3 temp = rocket.position;
        temp.z = temp.z - 25;
        temp.y = temp.y + 1;

        transform.position = temp;


        transform.LookAt(rocket, Vector3.up);

        //Vector3 lookOnObject = rocket.position - (rocket.up * 5);

        //transform.forward = lookOnObject.normalized;


        //Vector3 playerLastPosition = rocket.position - lookOnObject.normalized * distanceFromObject;

        if (Input.GetKeyDown("escape"))
        {
            //When a key is pressed down it see if it was the escape key if it was it will execute the code
            Application.Quit(); // Quits the game

        }




    }

}

