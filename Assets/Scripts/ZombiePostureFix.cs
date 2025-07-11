using UnityEngine;
using UnityEngine.AI;

public class ZombiePostureFix : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;

    [Header("Posture Settings")]
    public float stoppingRadius = 3.5f; // Adjust to match NavMeshAgent's stopping distance
    public float uprightForce = 5f;     // Strength to resist leaning

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true; // Completely lock physics rotations
        }
    }

    void Update()
    {
        // Force upright rotation (zero out X/Z rotations)
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        // Extra stabilization near stopping distance
        if (agent.remainingDistance <= stoppingRadius)
        {
            StabilizePosture();
        }
    }

    void StabilizePosture()
    {
        // Add downward force to prevent backward lean
        if (rb != null)
        {
            rb.AddForce(Vector3.down * uprightForce, ForceMode.Force);
        }

        // Snap to exact upright position
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
}