using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        // Quit the application (only works in standalone builds)

        Debug.Log("Quit");
        // Output a log message indicating that the quit action has been triggered
    }
}
