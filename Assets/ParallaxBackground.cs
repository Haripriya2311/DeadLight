using UnityEngine;
using UnityEngine.UI;

public class ParallaxSpriteBackground : MonoBehaviour
{
    public Image backgroundImage;
    public float zoomSpeed = 0.005f;      // 🔸 Slow zoom
    public float maxZoom = 1.05f;         // 🔸 Max zoom scale
    public float minZoom = 0.95f;         // 🔸 Min zoom scale

    private RectTransform rectTransform;
    private Vector3 initialScale;
    private float zoomDirection = 1f;

    void Start()
    {
        rectTransform = backgroundImage.GetComponent<RectTransform>();
        initialScale = rectTransform.localScale;
    }

    void Update()
    {
        if (backgroundImage == null || rectTransform == null) return;

        // 🔁 Animate zoom in/out
        float scaleChange = zoomSpeed * zoomDirection;
        Vector3 newScale = rectTransform.localScale + new Vector3(scaleChange, scaleChange, 0f);

        rectTransform.localScale = newScale;

        // 🔄 Reverse direction if limits reached
        if (newScale.x >= maxZoom * initialScale.x || newScale.x <= minZoom * initialScale.x)
        {
            zoomDirection *= -1f;
        }
    }
}
