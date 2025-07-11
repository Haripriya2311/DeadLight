using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Assign 12x12 image in Inspector
    public Vector2 hotSpot = new Vector2(6, 6); // ✅ Center of 12x12
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}