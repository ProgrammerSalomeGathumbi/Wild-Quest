using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void ReloadLevel()
    {
        // Reload the current scene by using SceneManager.LoadScene and passing the name of the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
