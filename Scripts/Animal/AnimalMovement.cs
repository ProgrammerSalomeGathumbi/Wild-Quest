using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Movement speed of the animal
    [SerializeField] private float jumpPower; // Power of the animal's jump
    [SerializeField] private LayerMask groundLayer; // Layer mask for the ground
    [SerializeField] private LayerMask wallLayer; // Layer mask for walls

    [Header("Sound")]
    [SerializeField] private AudioSource jumpSoundEffect; // Sound effect played when jumping

    private Rigidbody2D body; // Reference to the Rigidbody2D component
    private Animator anim; // Reference to the Animator component
    private BoxCollider2D boxCollider; // Reference to the BoxCollider2D component
    private float wallJumpCooldown; // Cooldown for wall jumps
    private float horizontalInput; // Horizontal input value

    private void Awake()
    {
        // Get references to the Rigidbody2D, Animator, and BoxCollider2D components attached to the same game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input value

        // Flip the player when moving left or right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameter for grounded state
        anim.SetBool("grounded", isGrounded());

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower); // Apply jump power to the vertical velocity
            anim.SetTrigger("jump"); // Trigger the "jump" animation

            jumpSoundEffect.Play(); // Play the jump sound effect
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                // Wall jump off a still wall
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6); // Wall jump while moving horizontally

            wallJumpCooldown = 0; // Reset the wall jump cooldown
        }
    }

    private bool isGrounded()
    {
        // Check if a box cast from the center of the BoxCollider2D towards the ground layer hits any collider
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null; // Return true if the box cast hits a collider, indicating that the animal is grounded
    }

    private bool onWall()
    {
        // Check if a box cast from the center of the BoxCollider2D towards the wall layer hits any collider
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null; // Return true if the box cast hits a collider, indicating that the animal is on a wall
    }

    public bool canAttack()
    {
        // Check if the animal is not moving horizontally, grounded, and not on a wall
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
