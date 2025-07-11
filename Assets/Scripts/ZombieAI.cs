using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float attackRange = 2f;
    public float damage = 10f;
    public float attackCooldown = 1.5f;

    private float nextAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // Chase the player
            agent.isStopped = false;
            agent.SetDestination(player.position);
            if (animator) animator.SetBool("isWalking", true);
        }
        else
        {
            // Attack
            agent.isStopped = true;
            if (animator) animator.SetBool("isWalking", false);

            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Zombie Attacking!");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        if (animator) animator.SetTrigger("attack");
    }
}

