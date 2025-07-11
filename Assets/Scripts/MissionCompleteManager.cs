using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionCompleteManager : MonoBehaviour
{
    public GameObject missionCompletePanel;
    public GameObject crosshair;
    public GameObject[] gameplayUI; // 🎯 Assign all gameplay UI elements here (e.g., timer, ammo, score)

    [Header("Buttons")]
    public Button nextLevelButton;
    public Button retryButton;
    public Button exitButton;

    [Header("Custom Cursor")]
    public Texture2D cursorTexture;
    public Vector2 hotSpot = new Vector2(16, 16);
    public CursorMode cursorMode = CursorMode.Auto;

    private bool missionCompleted = false;

    public static MissionCompleteManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // 🔒 Hide cursor at start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        // ✅ Assign button listeners
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(OnNextLevelClicked);

        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetryClicked);

        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitClicked);
    }

    public void TriggerMissionComplete()
    {
        if (missionCompleted) return;
        missionCompleted = true;

        // ✅ Show the panel
        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(true);

        // ❌ Hide crosshair
        if (crosshair != null)
            crosshair.SetActive(false);

        // ❌ Hide gameplay UI
        foreach (GameObject ui in gameplayUI)
        {
            if (ui != null)
                ui.SetActive(false);
        }

        // 🧊 Pause game
        Time.timeScale = 0f;
        AudioListener.volume = 0f;

        // 🟢 Show cursor
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("✅ Mission Completed!");
    }

    // ▶ NEXT LEVEL
    void OnNextLevelClicked()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0; // Go back to first if no next level

        SceneManager.LoadScene(nextSceneIndex);
    }

    // 🔁 RETRY
    void OnRetryClicked()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 🏠 EXIT
    void OnExitClicked()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        SceneManager.LoadScene("MainMenu"); // 🔁 Make sure the Main Menu scene is added to Build Settings
    }
}
