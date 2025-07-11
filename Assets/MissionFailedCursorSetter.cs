using UnityEngine;

public class MissionFailedCursorSetter : MonoBehaviour
{
    [Header("Custom Cursor Texture")]
    public Texture2D cursorTexture; // 🎯 Assign your 32x32 or similar cursor image
    public Vector2 hotSpot = new Vector2(16, 16); // 🎯 Center of 32x32 image
    public CursorMode cursorMode = CursorMode.Auto;

    void OnEnable()
    {
        // 🟢 Show the system cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // 🟢 Apply the custom cursor
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            Debug.LogWarning("⚠️ Custom cursor texture not assigned!");
        }
    }
}
