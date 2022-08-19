using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHolder : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                LevelSuccessSequense();
                break;
            //todo add fuel mechanic
            default:
                StartCrashSequense();
                break;
        }
    }

    void StartCrashSequense()
    {
        //todo add SFX upon crash
        //todo add particle upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LevelSuccessSequense()
    {
        //todo add SFX upon tuch finish
        //todo add particle upon tuch finish
        GetComponent<Movement>().enabled = false;
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
