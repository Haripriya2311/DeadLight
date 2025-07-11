using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    public string nextSceneName = "MainMenu"; // 🔸 Update with your actual main menu scene name

    private bool keyPressed = false;

    void Update()
    {
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
