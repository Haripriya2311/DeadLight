using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar; // Drag your UI Slider here in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void TakeDamagePercentage(float percentage)
    {
        float amount = maxHealth * (percentage / 100f);
        TakeDamage(amount);
    }


    void Die()
    {
        Debug.Log("Player Died");

        // 🔴 NEW: Trigger mission failed if player dies
        if (MissionFailedManager.Instance != null)
        {
            MissionFailedManager.Instance.TriggerMissionFailed("Player is dead"); // 🔴
        }
        else
        {
            Debug.LogWarning("❌ MissionFailManager is null!"); // 🔴
        }

        // (You can add player death animation or UI later here)
    }
}
