using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance; // Distance the enemy moves from its initial position
    [SerializeField] private float speed; // Speed of the enemy's movement
    [SerializeField] private float damage; // Amount of damage inflicted by the enemy
    private bool movingLeft; // Flag to check if the enemy is moving left
    private float leftEdge; // Leftmost position of the enemy's movement range
    private float rightEdge; // Rightmost position of the enemy's movement range

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                // Move the enemy to the left
            }
            else
                movingLeft = false; // Change direction when reaching the left edge
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                // Move the enemy to the right
            }
            else
                movingLeft = true; // Change direction when reaching the right edge
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Animal")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            // Damage the animal if it collides with the enemy
        }
    }
}
