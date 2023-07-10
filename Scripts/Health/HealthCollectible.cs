using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue; // The amount of health the collectible provides
    [Header("Sound")]
    [SerializeField] private AudioSource collectSoundEffect; // Sound effect played when the collectible is collected

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Animal") // Check if the collider belongs to the "Animal" tag
        {
            collectSoundEffect.Play(); // Play the collect sound effect
            collision.GetComponent<Health>().AddHealth(healthValue); // Access the Health component on the collided object and add healthValue to its current health
            gameObject.SetActive(false); // Deactivate the collectible object
        }
    }
}
