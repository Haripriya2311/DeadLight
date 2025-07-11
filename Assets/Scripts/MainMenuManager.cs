using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Example: Load loading scene
        SceneManager.LoadScene("LoadingScene"); // Replace with your actual loading scene name
    }

    public void ExitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

#if UNITY_EDITOR
        // If testing in the Unity editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
