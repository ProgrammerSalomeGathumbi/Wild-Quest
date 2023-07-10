using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth; // Starting health value
    public float currentHealth { get; private set; } // Current health value (read-only property)
    private Animator anim; // Reference to the Animator component
    private bool dead; // Flag indicating if the object is dead

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration; // Duration of invulnerability frames
    [SerializeField] private int numberOfFlashes; // Number of times the object should flash during invulnerability
    private SpriteRenderer spriteRend; // Reference to the SpriteRenderer component

    [Header("Components")]
    [SerializeField] private Behaviour[] components; // Array of components to deactivate when the object dies
    private bool invulnerable; // Flag indicating if the object is currently invulnerable

    [Header("Sound")]
    [SerializeField] private AudioSource deathSoundEffect; // Sound effect played when the object dies
    [SerializeField] private AudioSource hurtSoundEffect; // Sound effect played when the object takes damage

    private void Awake()
    {
        currentHealth = startingHealth; // Set the current health to the starting health
        anim = GetComponent<Animator>(); // Get the Animator component attached to the same game object
        spriteRend = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the same game object
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return; // If the object is currently invulnerable, do not take damage

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // Reduce the current health by the specified damage amount

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt"); // Trigger the "hurt" animation state in the Animator component
            StartCoroutine(Invunerability()); // Start the invulnerability coroutine
            hurtSoundEffect.Play(); // Play the hurt sound effect
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die"); // Trigger the "die" animation state in the Animator component

                // Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true; // Set the dead flag to true
                deathSoundEffect.Play(); // Play the death sound effect
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); // Increase the current health by the specified value within the range of 0 to the starting health
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true; // Set the invulnerable flag to true
        Physics2D.IgnoreLayerCollision(8, 9, true); // Ignore collision between layer 8 and layer 9

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Set the sprite color to red with reduced alpha
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // Wait for half the invulnerability duration
            spriteRend.color = Color.white; // Reset the sprite color to white
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // Wait for the other half of the invulnerability duration
        }

        Physics2D.IgnoreLayerCollision(8, 9, false); // Stop ignoring collision between layer 8 and layer 9
        invulnerable = false; // Set the invulnerable flag to false
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // Deactivate the game object
    }
}
