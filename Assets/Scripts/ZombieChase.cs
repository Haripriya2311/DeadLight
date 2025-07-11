using UnityEngine;

public class ZombieChaseSimple : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1.5f;
    public float stopDistance = 2f;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody attached to this zombie
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        Vector3 lookDirection = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookDirection);

        if (distance > stopDistance)
        {
            Vector3 moveDirection = transform.forward;
            Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
