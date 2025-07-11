using UnityEngine;
using UnityEngine.AI;

public class ZombieFollow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [Header("Attack Settings")]
    public float stopDistance = 2.5f;  // Distance at which zombie stops moving

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Optional: prevent automatic tilting
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
            FacePlayer();
        }
    }

    void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 euler = lookRotation.eulerAngles;
            euler.x = 0f; // prevent leaning
        }
    }
}