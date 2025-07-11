using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [Header("Explosion Settings")]
    public int hitsToExplode = 3;
    private int currentHits = 0;
    public float explosionRadius = 5f;
    public GameObject explosionEffect;
    private bool hasExploded = false;

    [Header("Damage Settings")]
    public float playerDamagePercent = 0.5f;     // 50% of player health
    public float normalZombieDamagePercent = 0.5f; // 50% of normal zombie health
    public float bossBarrelDamage = 500f;        // 💥 Boss takes 500 damage

    public void TakeHit()
    {
        if (hasExploded) return;

        currentHits++;
        Debug.Log("🧨 Barrel hit! Current hits: " + currentHits);

        if (currentHits >= hitsToExplode)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;
        Debug.Log("💥 Barrel exploded!");

        // Disable visuals and collider
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null) mr.enabled = false;

        foreach (Transform child in transform)
        {
            Collider childCol = child.GetComponent<Collider>();
            if (childCol != null) childCol.enabled = false;

            MeshRenderer childMR = child.GetComponent<MeshRenderer>();
            if (childMR != null) childMR.enabled = false;
        }

        // Play explosion VFX
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // 💥 Damage nearby objects
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            // 🔴 Damage Player
            if (hit.CompareTag("Player"))
            {
                PlayerHealth ph = hit.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    float damage = ph.maxHealth * playerDamagePercent;
                    ph.TakeDamage(damage);
                    Debug.Log("🔥 Damaged Player: " + damage);
                }
            }

            // 🧟 Damage Normal Zombies
            if (hit.CompareTag("Enemy"))
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    float damage = eh.maxHealth * normalZombieDamagePercent;
                    eh.TakeDamage(damage);
                    Debug.Log("🧟 Damaged Normal Zombie: " + damage);
                }
            }

            // 👑 Damage Boss Zombie
            BossZombieBehaviour boss = hit.GetComponentInParent<BossZombieBehaviour>();
            if (boss != null)
            {
                boss.TakeDamage(bossBarrelDamage, true);
                Debug.Log("👑 Boss Zombie took " + bossBarrelDamage + " damage from barrel!");
            }
        }

        // Destroy barrel after short delay
        Destroy(gameObject, 1.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
