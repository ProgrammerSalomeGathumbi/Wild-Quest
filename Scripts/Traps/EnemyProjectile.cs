using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed; // Speed of the projectile
    [SerializeField] private float resetTime; // Time before the projectile is reset
    private float lifetime; // Time since the projectile was activated
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0); // Move the projectile forward

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false); // Deactivate the projectile after a certain lifetime
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); // Execute logic from parent script first
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("attack");
        else
            gameObject.SetActive(false); // Deactivate the projectile when it hits any object
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
