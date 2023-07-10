using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound instance { get; private set; } // Singleton instance of the Sound class
    private AudioSource source; // Reference to the AudioSource component

    private void Awake()
    {
        instance = this; // Set the singleton instance to this Sound instance
        source = GetComponent<AudioSource>(); // Get the AudioSource component attached to the same game object
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound); // Play the specified sound effect using PlayOneShot method of the AudioSource component
    }
}
