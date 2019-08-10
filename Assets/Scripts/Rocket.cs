using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource audioSource;
    [SerializeField] float rotationPower = 100f;
    [SerializeField] float thrustPower = 100f;
    [SerializeField] private float levelLoadDelay = 2f;

    [SerializeField] AudioClip engineSound = default;
    [SerializeField] AudioClip deathSound = default;
    [SerializeField] AudioClip winSound = default;

    [SerializeField] ParticleSystem engineParticles = default;
    [SerializeField] ParticleSystem deathParticles = default;
    [SerializeField] ParticleSystem winParticles = default;
    [SerializeField] ParticleSystem cumParticles = default;

    enum State { Alive, Dying, Transcending };
    enum DebugState { Normal, Immortality };

    private State state = State.Alive;

    private bool collisionsDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
    }

    // Update is called once per frame
    void Update()
    {

        Controls();


        print(state);
    }


    void Controls()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        else if (state == State.Dying)
        {
            _rigidbody.freezeRotation = false;

        }

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }

        

    }

    private void LoadNextLevelDebug()
    {
        
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;

        } else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
    }


    void OnCollisionEnter(Collision collision)
    {

        if (state != State.Alive || collisionsDisabled)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        deathParticles.Play();
        audioSource.PlayOneShot(deathSound);
        engineParticles.Stop();
        cumParticles.Stop();
        Invoke("RestartGame", levelLoadDelay);
    }

    private void StartFinishSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        winParticles.Play();
        audioSource.PlayOneShot(winSound);
        engineParticles.Stop();
        cumParticles.Stop();
        Invoke("LoadNextScene", levelLoadDelay);
    }




    private void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
    }




    private void LoadNextScene()
    {
        int numberOfLevels = SceneManager.sceneCountInBuildSettings;
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        if (nextLevel == numberOfLevels)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
        
    }





    private void Thrust()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            engineParticles.Play();
            cumParticles.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            engineParticles.Stop();
            cumParticles.Stop();
        }



        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame = thrustPower * Time.deltaTime;
            _rigidbody.freezeRotation = true;
            _rigidbody.AddRelativeForce(Vector3.up * thrustThisFrame);
            

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineSound);
            }
            
            
        }
        else
        {
            
            audioSource.Stop();
            _rigidbody.freezeRotation = false;
            
            
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
