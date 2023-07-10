using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage; // Amount of damage inflicted by the enemy

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Animal")
            collision.GetComponent<Health>().TakeDamage(damage); // Damage the animal if it collides with the enemy
    }
}
