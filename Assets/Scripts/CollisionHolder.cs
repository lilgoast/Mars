using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHolder : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    
    AudioSource audioSource;
    Movement movement;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if(GetComponent<FuelMechanic>().enabled == false)
                GetComponent<FuelMechanic>().enabled = true;
            else
                GetComponent<FuelMechanic>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequense();
                break;
            case "Fuel":
                break;
            default:
                StartCrashSequense();
                break;
        }
    }

    void StartCrashSequense()
    {
        isTransitioning = true;

        GetComponent<Movement>().enabled = false;
        movement.StopTrusting();
        movement.StopRotation();

        crashParticles.Play();
        audioSource.PlayOneShot(crash);

        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequense()
    {
        isTransitioning = true;

        GetComponent<Movement>().enabled = false;

        movement.StopTrusting();
        movement.StopRotation();

        successParticles.Play();
        audioSource.PlayOneShot(success);

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
