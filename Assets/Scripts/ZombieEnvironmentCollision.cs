using UnityEngine;

public class ZombieEnvironmentCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            Debug.Log("Zombie collided with environment: " + collision.gameObject.name);
            // Optionally, play sound, push back, or stop movement briefly
        }
    }
}
