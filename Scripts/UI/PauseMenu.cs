using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        // Activate the pause menu UI

        Time.timeScale = 0f;
        // Set the time scale to 0 to pause the game
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        // Deactivate the pause menu UI

        Time.timeScale = 1f;
        // Set the time scale back to 1 to resume the game
    }

    public void Start()
    {
        SceneManager.LoadScene("StartMenu");
        // Load the "StartMenu" scene
    }

    public void Quit()
    {
        Application.Quit();
        // Quit the application (only works in standalone builds)
    }
}
