using UnityEngine;

public class ShowCursorInIntro : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // 🔓 Unlock the mouse
        Cursor.visible = true;                  // 👁️ Show the mouse
    }
}
