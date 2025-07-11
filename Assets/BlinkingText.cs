using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI textToBlink;
    public float blinkSpeed = 1f;

    private Color originalColor;

    void Start()
    {
        if (textToBlink != null)
            originalColor = textToBlink.color;
    }

    void Update()
    {
        if (textToBlink != null)
        {
            float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f; // value from 0 to 1
            Color newColor = originalColor;
            newColor.a = alpha;
            textToBlink.color = newColor;
        }
    }
}
