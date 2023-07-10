using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge; // Left patrol point
    [SerializeField] private Transform rightEdge; // Right patrol point

    [Header("Enemy")]
    [SerializeField] private Transform enemy; // Enemy's transform

    [Header("Movement parameters")]
    [SerializeField] private float speed; // Movement speed of the enemy
    private Vector3 initScale; // Initial scale of the enemy
    private bool movingLeft; // Flag indicating if the enemy is moving left

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; // Duration of idle time after reaching a patrol point
    private float idleTimer; // Timer for idle duration

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim; // Animator component of the enemy

    private void Awake()
    {
        initScale = enemy.localScale; // Store the initial scale of the enemy
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false); // Set the "moving" parameter in the animator to false when disabled
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1); // Move left if not reached the left patrol point
            else
                DirectionChange(); // Change direction when reached the left patrol point
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1); // Move right if not reached the right patrol point
            else
                DirectionChange(); // Change direction when reached the right patrol point
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false); // Set the "moving" parameter in the animator to false
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft; // Change the direction flag and reset the idle timer
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true); // Set the "moving" parameter in the animator to true

        // Make the enemy face the specified direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        // Move the enemy in the specified direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
