using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void OverScene()
    {
        SceneManager.LoadScene("GameOver");
        // Load the "GameOver" scene
    }
}
