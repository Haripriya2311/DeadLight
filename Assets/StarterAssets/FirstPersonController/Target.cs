using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [Header("Enemy Health")]
    public float health = 200f;  // Total health (adjust as needed)
    public Slider healthSlider; // Drag the health bar slider here in Inspector

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (healthSlider != null)
            healthSlider.value = health;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
