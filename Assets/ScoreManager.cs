using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // 🔁 Singleton

    [Header("Score & Enemy Info")]
    public int score = 0;
    public int enemiesRemaining = 20;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesText;

    void Awake()
    {
        // ✅ Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
        UpdateEnemiesUI();
    }

    // ✅ Increases score
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }

    // ✅ Decreases enemies
    public void DecreaseEnemies(int value)
    {
        enemiesRemaining -= value;
        if (enemiesRemaining < 0) enemiesRemaining = 0;
        UpdateEnemiesUI();

        if (enemiesRemaining == 0) // 🟢 NEW: Trigger mission complete
        {
            MissionCompleteManager.Instance.TriggerMissionComplete(); // 🟢 NEW
        }
    }

    // ✅ Updates score UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    // ✅ Updates enemies UI
    private void UpdateEnemiesUI()
    {
        if (enemiesText != null)
            enemiesText.text = "Enemies Left: " + enemiesRemaining;
    }
}