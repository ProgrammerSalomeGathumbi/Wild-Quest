using UnityEngine;

public class AnimalAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform woolPoint;
    [SerializeField] private GameObject[] woolballs;
    
    private Animator anim;
    private AnimalMovement animalMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        animalMovement = GetComponent<AnimalMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && animalMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        woolballs[FindWoolball()].transform.position = woolPoint.position;
        woolballs[FindWoolball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindWoolball()
    {
        for (int i = 0; i < woolballs.Length; i++)
        {
            if (!woolballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}