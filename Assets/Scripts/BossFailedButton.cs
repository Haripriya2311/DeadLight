using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFailedButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void RetryGame()
    {
        Time.timeScale = 1f; // ⏱️ Resume time
        AudioListener.volume = 1f; // 🔊 Resume audio
        SceneManager.LoadScene("BossFightScene");
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

