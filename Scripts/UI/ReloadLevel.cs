using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Reload the current scene to restart the level

        Debug.Log("Restart");
        // Output a log message indicating that the level restart has been triggered
    }
}
