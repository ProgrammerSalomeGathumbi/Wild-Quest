using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Level 1");
        // Load the "Level 1" scene to restart the game
    }
}
