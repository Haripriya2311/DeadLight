using UnityEngine;
using TMPro;

public class MissionFailedManager : MonoBehaviour
{
    public GameObject missionFailedPanel;
    public TextMeshProUGUI reasonText;
    public GameObject crosshair;
    public GameObject gameplayUI;

    private bool hasFailed = false;

    public static MissionFailedManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // 🔴 Ensure default cursor is hidden at game start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TriggerMissionFailed(string reason)
    {
        if (hasFailed) return;
        hasFailed = true;

        if (missionFailedPanel != null)
            missionFailedPanel.SetActive(true);

        if (reasonText != null)
            reasonText.text = reason;

        if (crosshair != null)
            crosshair.SetActive(false); // 🟡 Hide crosshair

        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        Time.timeScale = 0f;           // 🟡 Freeze the game
        AudioListener.volume = 0f;     // 🟡 Mute all game sound

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("💀 Mission Failed: " + reason);
    }
}
