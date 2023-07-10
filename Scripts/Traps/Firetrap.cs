using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage; // Amount of damage inflicted by the firetrap

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay; // Delay before activating the firetrap
    [SerializeField] private float activeTime; // Duration of the active state of the firetrap

    [Header("Sound")]
    [SerializeField] private AudioSource firetrapSoundEffect; // Sound effect played by the firetrap

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; // Flag to check if the firetrap has been triggered
    private bool active; // Flag to check if the firetrap is active and can hurt the player

    private Health animalHealth; // Reference to the health component of the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (animalHealth != null && active)
        {
            animalHealth.TakeDamage(damage);
            // Damage the player if they are in contact with the active firetrap
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Animal")
        {
            animalHealth = collision.GetComponent<Health>();
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
            
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Animal")
        {
            animalHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color = Color.red;
        // Turn the sprite red to notify the player and indicate the firetrap has been triggered

        yield return new WaitForSeconds(activationDelay);
        firetrapSoundEffect.Play();
        // Wait for the activation delay, play the firetrap sound effect

        spriteRend.color = Color.white;
        // Reset the sprite color to its initial state

        active = true;
        anim.SetBool("activated", true);
        // Activate the firetrap and play the animation

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
        // Wait for the active time, deactivate the firetrap and reset the variables and animator
    }
}
