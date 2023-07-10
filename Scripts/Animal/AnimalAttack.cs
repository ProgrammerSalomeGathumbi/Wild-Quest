using UnityEngine;

public class AnimalAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown; // Cooldown time between attacks
    [SerializeField] private Transform woolPoint; // The point where woolballs will be spawned
    [SerializeField] private GameObject[] woolballs; // Array of woolball game objects

    [Header("Sound")]
    [SerializeField] private AudioSource woolSoundEffect; // Sound effect played when attacking

    private Animator anim; // Reference to the animator component
    private AnimalMovement animalMovement; // Reference to the AnimalMovement script
    private float cooldownTimer = Mathf.Infinity; // Timer to track the cooldown time

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Get the Animator component attached to the same game object
        animalMovement = GetComponent<AnimalMovement>(); // Get the AnimalMovement script attached to the same game object
    }

    private void Update()
    {
        // Check if the left mouse button is pressed, cooldown timer has exceeded the attack cooldown, and the animal can attack
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && animalMovement.canAttack())
        {
            Attack(); // Call the Attack method
        }

        cooldownTimer += Time.deltaTime; // Increment the cooldown timer with the elapsed time since the last frame
    }

    private void Attack()
    {
        anim.SetTrigger("attack"); // Trigger the "attack" animation state in the Animator component
        woolSoundEffect.Play(); // Play the wool sound effect
        cooldownTimer = 0; // Reset the cooldown timer

        // Find an inactive woolball from the woolballs array and set its position to the woolPoint
        woolballs[FindWoolball()].transform.position = woolPoint.position;

        // Set the direction of the woolball based on the sign of the local scale of the game object
        woolballs[FindWoolball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindWoolball()
    {
        for (int i = 0; i < woolballs.Length; i++)
        {
            // Find the first inactive woolball in the woolballs array and return its index
            if (!woolballs[i].activeInHierarchy)
                return i;
        }

        // If no inactive woolball is found, return 0 as a fallback
        return 0;
    }
}
