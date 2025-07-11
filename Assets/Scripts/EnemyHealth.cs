using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    float currentHealth;

    public Slider healthSlider;
    public GameObject deathEffect;

    private bool isDead = false; // ✅ Prevent multiple deaths

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // ✅ Don’t take damage after death

        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // ✅ Prevent this from running again

        isDead = true; // ✅ Mark as dead

        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        ZombieBehavior zombie = GetComponent<ZombieBehavior>();
        if (zombie != null)
        {
            zombie.Die(); // ✅ Handles score + destroy
        }

        // ❌ No need to Destroy here again — handled in ZombieBehavior
    }
}
