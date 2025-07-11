using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BossDefeatedManager : MonoBehaviour
{
    public static BossDefeatedManager Instance;

    [Header("Boss Defeated UI")]
    public GameObject bossDefeatedPanel;        // 🟥 Panel with Boss Defeated image
    public Button replayButton;                 // 🔁 Reload Game Scene
    public Button exitButton;                   // 🔙 Go to Main Menu
    public TextMeshProUGUI bossDefeatedText;    // Optional: Boss defeated label

    [Header("Custom Cursor")]
    public Texture2D cursorTexture;
    public Vector2 cursorHotspot = Vector2.zero;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (bossDefeatedPanel != null)
            bossDefeatedPanel.SetActive(false);
    }

    public void ShowBossDefeatedPanel()
    {
        if (bossDefeatedPanel != null)
        {
            bossDefeatedPanel.SetActive(true);
            Time.timeScale = 0f; // 🕒 Pause the game
        }

        // 🖱️ Set custom cursor AFTER enabling the panel
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
            Debug.Log("🖱️ Custom cursor set by BossDefeatedManager.");
        }
        else
        {
            Debug.LogWarning("⚠️ Cursor texture not assigned in BossDefeatedManager!");
        }

        // Add button listeners
        if (replayButton != null)
            replayButton.onClick.AddListener(ReplayGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitToMainMenu);
    }

    void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene"); // 🎮 Your actual gameplay scene name
    }

    void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // 🏠 Your main menu scene name
    }
}
