using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Timer : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;

    public int Duration;
    private int remainingDuration;
    private bool Pause;

    // 🟥 MISSION FAILED UI
    public GameObject missionFailedPanel;
    public TextMeshProUGUI reasonText;

    // 🟥 Objects to disable
    public GameObject playerObject;         // 🎮 FPS player
    public GameObject zombieParentObject;   // 🧟 Parent of all zombies
    public GameObject gameplayUI;           // 📊 Timer, ammo, health bar, etc.

    private void Start()
    {
        Being(Duration);
    }

    private void Being(int seconds)
    {
        remainingDuration = seconds;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        // ✅ Show Mission Failed UI
        if (missionFailedPanel != null && reasonText != null)
        {
            reasonText.text = "Mission Failed: Time's Up!";
            missionFailedPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("❌ Assign MissionFailedPanel and ReasonText!");
        }

        // ❌ Disable Player
        if (playerObject != null)
            playerObject.SetActive(false);
        else
            Debug.LogWarning("⚠️ playerObject not assigned!");

        // ❌ Disable Zombies
        if (zombieParentObject != null)
            zombieParentObject.SetActive(false);
        else
            Debug.LogWarning("⚠️ zombieParentObject not assigned!");

        // ❌ Hide Gameplay UI
        if (gameplayUI != null)
            gameplayUI.SetActive(false);
        else
            Debug.LogWarning("⚠️ gameplayUI not assigned!");
    }
}
