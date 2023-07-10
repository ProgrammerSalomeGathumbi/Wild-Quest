using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the projectile
    private float direction; // Direction of the projectile
    private bool hit; // Flag to check if the projectile has hit something
    private float lifetime; // Time since the projectile was activated

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0); // Move the projectile in the specified direction

        lifetime += Time.deltaTime;
        if (lifetime > 2) gameObject.SetActive(false); // Deactivate the projectile after a certain lifetime
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>()?.TakeDamage(1); // Damage the enemy if it is hit by the projectile
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
