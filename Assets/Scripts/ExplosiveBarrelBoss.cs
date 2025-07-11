using UnityEngine;

public class ExplosiveBarrelBoss : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float zombieDamage = 50f;
    public float playerDamage = 50f;
    public GameObject explosionEffect;
    public int shotsToExplode = 3;

    private int currentShots = 0;
    private bool hasExploded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !hasExploded)
        {
            currentShots++;
            if (currentShots >= shotsToExplode)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        hasExploded = true;

        // Optional explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // ✅ Regular Zombies
            ZombieHealth zombieHealth = nearbyObject.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(zombieDamage);
            }

            // ✅ Boss Zombie
            if (nearbyObject.CompareTag("BossZombie"))
            {
                BossZombieHealth bossHealth = nearbyObject.GetComponent<BossZombieHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(zombieDamage);
                }
            }

            // ✅ Player
            if (nearbyObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(playerDamage);
                }
            }
        }

        Destroy(gameObject); // Remove the barrel after explosion
    }
}
