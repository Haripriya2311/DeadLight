using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossZombieHealth : MonoBehaviour
{
    public float maxHealth = 7500f;
    public float currentHealth;

    public Slider bossHealthSlider; // Assign this in Inspector

    [Header("UI")]
    public GameObject bossDefeatedPanel; // 🔴 Assign in Inspector

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (bossHealthSlider != null)
        {
            bossHealthSlider.maxValue = maxHealth;
            bossHealthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("⚠️ bossHealthSlider is not assigned in the Inspector!");
        }

        if (bossDefeatedPanel != null)
            bossDefeatedPanel.SetActive(false); // Hide at start
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("✅ Boss took damage: " + damage);

        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (bossHealthSlider != null)
        {
            bossHealthSlider.value = currentHealth;
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("💀 Boss Zombie Dead");

        // ⏳ Start coroutine to show panel after delay
        StartCoroutine(ShowBossDefeatedAfterDelay());

        // (Optional) You can trigger boss death animation here
    }

    IEnumerator ShowBossDefeatedAfterDelay()
    {
        yield return new WaitForSeconds(7f);

        if (bossDefeatedPanel != null)
        {
            bossDefeatedPanel.SetActive(true);
            Time.timeScale = 0f; // Optional: Pause the game
        }
        else
        {
            Debug.LogWarning("⚠️ BossDefeatedPanel is not assigned!");
        }
    }
}
