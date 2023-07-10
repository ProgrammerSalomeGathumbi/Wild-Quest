using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown; // Cooldown time between attacks
    [SerializeField] private Transform firePoint; // Point from where arrows are fired
    [SerializeField] private GameObject[] arrows; // Array of arrow objects
    [Header("Sound")]
    [SerializeField] private AudioSource arrowSoundEffect; // Sound effect played when arrows are fired
    private float cooldownTimer; // Timer to track attack cooldown

    private void Attack()
    {
        cooldownTimer = 0;

        arrowSoundEffect.Play(); // Play arrow sound effect
        arrows[FindArrow()].transform.position = firePoint.position; // Set arrow position to fire point
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile(); // Activate arrow projectile
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i; // Return index of inactive arrow object
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack(); // Trigger attack if cooldown time has elapsed
    }
}
