using UnityEngine;
using UnityEngine.AI;

public class BossZombieBehaviour : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 20f;
    public float attackRadius = 2.5f;
    public float moveSpeed = 2f;
    public float attackCooldown = 2f;

    private float lastAttackTime;
    private bool isDead = false;

    private Animator animator;
    private NavMeshAgent agent;
    private BossZombieHealth bossHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        bossHealth = GetComponent<BossZombieHealth>();

        if (agent != null)
        {
            agent.speed = moveSpeed;
            agent.stoppingDistance = attackRadius;
            agent.updateRotation = true; // ✅ Auto rotation
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRadius)
        {
            AttackPlayer();
        }
        else if (distance <= detectionRadius)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        animator.SetBool("Walk", true);
        animator.ResetTrigger("Attack");

        if (agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            // ✅ Manual move & rotate
            Vector3 dir = (player.position - transform.position).normalized;
            dir.y = 0;

            if (dir != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
            }

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void StopMoving()
    {
        animator.SetBool("Walk", false);
        if (agent != null) agent.isStopped = true;
    }

    void AttackPlayer()
    {
        animator.SetBool("Walk", false);

        // 🔁 Rotate toward player always, even with NavMeshAgent
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
        }

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;

            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(20f);
                Debug.Log("👊 Boss attacked player for 20");
            }
        }

        if (agent != null) agent.isStopped = true;
    }




    public void TakeDamage(float damage, bool fromBarrel)
    {
        if (isDead) return;

        if (fromBarrel)
        {
            animator.SetTrigger("Hit");
            Debug.Log("👑 Boss reacts to barrel hit");
        }

        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);

            if (bossHealth.currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        StopMoving();
        animator.SetTrigger("Die");
        Debug.Log("☠️ Boss zombie is dead");

        // Use animation event or coroutine
        // StartCoroutine(WaitThenDestroy()); // ← enable this only if you're not using events
    }
}
