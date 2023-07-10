using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Reload the current scene to restart the game
    }

    public void Quit()
    {
        Application.Quit();
        // Quit the application (only works in standalone builds)
    }
}
