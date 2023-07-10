using UnityEngine;

public class DinosaurEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; // Cooldown time between attacks
    [SerializeField] private float range; // Range within which the enemy can detect the player
    [SerializeField] private int damage; // Damage inflicted by the enemy's attacks

    [Header("Slash Attack")]
    [SerializeField] private Transform slashpoint; // Transform representing the point where slash attacks are spawned
    [SerializeField] private GameObject[] slashes; // Array of slash attack game objects

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance; // Distance multiplier for the enemy's collider size
    [SerializeField] private BoxCollider2D boxCollider; // Reference to the enemy's box collider

    [Header("Animal Layer")]
    [SerializeField] private LayerMask animalLayer; // Layer mask for the player object

    [Header("Sound")]
    [SerializeField] private AudioSource woolSoundEffect; // Sound effect played when attacking

    // References
    private Animator anim; // Reference to the Animator component
    private Health animalHealth; // Reference to the player's health component
    private EnemyPatrol enemyPatrol; // Reference to the enemy patrol component

    private float cooldownTimer = Mathf.Infinity; // Timer to track the cooldown time

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Get the Animator component attached to the same game object
        enemyPatrol = GetComponentInParent<EnemyPatrol>(); // Get the EnemyPatrol component attached to the parent game object
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // Increment the cooldown timer with the elapsed time since the last frame

        // Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0; // Reset the cooldown timer
                anim.SetTrigger("dinosaurAttack"); // Trigger the dinosaurAttack animation state in the Animator component
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight(); // Enable/disable the enemy patrol based on whether the player is in sight
    }

    private void SlashAttack()
    {
        cooldownTimer = 0; // Reset the cooldown timer
        slashes[FindSlashes()].transform.position = slashpoint.position; // Find an inactive slash attack from the slashes array and set its position
        slashes[FindSlashes()].GetComponent<EnemyProjectile>().ActivateProjectile(); // Activate the slash attack projectile
    }

    private int FindSlashes()
    {
        for (int i = 0; i < slashes.Length; i++)
        {
            if (!slashes[i].activeInHierarchy)
                return i; // Find the first inactive slash attack in the slashes array and return its index
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        // Perform a box cast in the direction of the player to check if the player is within the range
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, animalLayer);

        return hit.collider != null; // Return true if the box cast hits the player, indicating that the player is in sight
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draw a wire cube representing the range of the enemy's box cast in the editor
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
