using UnityEngine;
using UnityEngine.SceneManagement;

public class BossCompletedButton : MonoBehaviour

{ 


    public void RetryGame()
    {
        Time.timeScale = 1f; // ⏱️ Resume time
        AudioListener.volume = 1f; // 🔊 Resume audio
        SceneManager.LoadScene("GameScene");
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}


