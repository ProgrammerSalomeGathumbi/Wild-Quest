using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShoot : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the attack
    private float direction; // Direction of the attack
    private bool hit; // Flag to check if attack has hit something
    private float lifetime; // Time since the attack was activated

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
        transform.Translate(movementSpeed, 0, 0); // Move the attack in the specified direction

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false); // Deactivate the attack after a certain lifetime
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("attack");

        if (collision.tag == "Animal")
            collision.GetComponent<Health>().TakeDamage(1); // Damage the animal if it is hit by the attack
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
    } // Set the local scale of the attack to ensure it faces the correct direction

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
