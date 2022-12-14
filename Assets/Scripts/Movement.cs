using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] int thrustSpeed = 100;
    [SerializeField] int rotationSpeed = 100;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem engineParticle;
    [SerializeField] ParticleSystem engineParticleLeft;
    [SerializeField] ParticleSystem engineParticleRight;

    [SerializeField] ParticleSystem rotationParticleLeft;
    [SerializeField] ParticleSystem rotationParticleRight;
 
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTrust();
        ProcessRotation();
    }

    void ProcessTrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartTrusting();
        }
        else
        {
            StopTrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StartTrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!engineParticle.isPlaying)
        {
            engineParticle.Play();
            engineParticleLeft.Play();
            engineParticleRight.Play();
        }

        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }
    public void StopTrusting()
    {
        audioSource.Stop();

        engineParticle.Stop();
        engineParticleRight.Stop();
        engineParticleLeft.Stop();
    }

    private void LeftRotation()
    {
        ApplyRotation(rotationSpeed);

        if (!rotationParticleLeft.isPlaying)
        {
            rotationParticleLeft.Play();
        }
    }
    private void RightRotation()
    {
        ApplyRotation(-rotationSpeed);

        if (!rotationParticleRight.isPlaying)
        {
            rotationParticleRight.Play();
        }
    }

    public void StopRotation()
    {
        rotationParticleRight.Stop();
        rotationParticleLeft.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
