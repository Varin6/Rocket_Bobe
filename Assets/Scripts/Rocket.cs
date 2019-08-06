using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    [SerializeField] float rotationPower = 100f;
    [SerializeField] float thrustPower = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
    }

    // Update is called once per frame
    void Update()
    {

        Thrust();
        Rotate();

    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            default:
                SceneManager.LoadScene("Level 1");
                //Application.LoadLevel(Application.loadedLevel);
                break;
        }
    }


    private void Thrust()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            rigidbody.freezeRotation = false;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame = thrustPower * Time.deltaTime;
            rigidbody.freezeRotation = true;
            rigidbody.AddRelativeForce(Vector3.up * thrustThisFrame);
            
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }

    }




    private void Rotate()
    {



        if (Input.GetKey(KeyCode.A))
        {
            float rotationThisFrame = rotationPower * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            float rotationThisFrame = rotationPower * Time.deltaTime;
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }



    }


}
