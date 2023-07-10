using UnityEngine;

public class DinosaurEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Slash Attack")]
    [SerializeField] private Transform slashpoint;
    [SerializeField] private GameObject[] slashes;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Animal Layer")]
    [SerializeField] private LayerMask animalLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Sound")]
    [SerializeField] private AudioSource woolSoundEffect;

    //References
    private Animator anim;
    private Health animalHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("dinosaurAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

    }
    private void SlashAttack()
    {

        cooldownTimer = 0;
        slashes[FindSlashes()].transform.position = slashpoint.position;
        slashes[FindSlashes()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindSlashes()
    {
        for (int i = 0; i < slashes.Length; i++)
        {
            if (!slashes[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, animalLayer);

         return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

}