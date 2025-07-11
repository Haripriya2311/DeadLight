using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    [Header("UI Components")]
    public Slider slider;                            // ✅ UI Slider
    public TextMeshProUGUI loadingText;              // ✅ TMP Text
    public TextMeshProUGUI tipText;                  // ✅ 🔥 Tip Text

    [Header("Loading Settings")]
    public float totalLoadTime = 10f;                // ⏱️ Total duration
    public float smoothSpeed = 0.2f;                 // 🌀 Smooth slider catch-up

    [Header("Typing Effect")]
    public string fullText = "Loading...";           // 🔤 Text to type
    public float typingSpeed = 0.02f;                // ⚡ Fast typing
    public float pauseTime = 0.5f;                   // ⏸️ Pause before blink
    public float blinkSpeed = 0.5f;                  // ✨ Blinking delay

    // 🔥 Tip messages
    private string[] tips = new string[]
    {
        "Tip: Shoot explosive barrels to take down multiple zombies quickly.",
        "Tip: Headshots deal more damage – aim for the brain.",
        "Tip: Keep moving to avoid zombie attacks.",
        "Tip: Reload when you're safe, not mid-fight!",
        "Tip: Explosions can hurt you too – stay back.",
        "Tip: Save bullets, aim wisely.",
        "Tip: Some zombies growl when close – listen carefully."
    };

    private float timer = 0f;
    private float actualProgress = 0f;
    private float visibleProgress = 0f;

    void Start()
    {
        slider.value = 0f;

        // ✅ Show one random tip (typing style)
        if (tipText != null && tips.Length > 0)
        {
            int randomIndex = Random.Range(0, tips.Length);
            StartCoroutine(TypeTip(tips[randomIndex]));
        }

        StartCoroutine(LoopTyping());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= 4f)
        {
            actualProgress = Mathf.Lerp(0f, 0.8f, timer / 4f);
        }
        else if (timer <= totalLoadTime)
        {
            float t = (timer - 4f) / 6f;
            actualProgress = Mathf.Lerp(0.8f, 1f, t);
        }
        else
        {
            actualProgress = 1f;
            LoadGameScene();
        }

        visibleProgress = Mathf.Lerp(visibleProgress, actualProgress, smoothSpeed);
        slider.value = visibleProgress;
    }

    IEnumerator LoopTyping()
    {
        while (true)
        {
            loadingText.text = "";

            for (int i = 0; i <= fullText.Length; i++)
            {
                loadingText.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(typingSpeed);
            }

            for (int b = 0; b < 4; b++)
            {
                loadingText.enabled = false;
                yield return new WaitForSeconds(blinkSpeed);
                loadingText.enabled = true;
                yield return new WaitForSeconds(blinkSpeed);
            }

            yield return new WaitForSeconds(pauseTime);
        }
    }

    IEnumerator TypeTip(string tip)
    {
        tipText.text = "";
        float delay = 8f / tip.Length; // 🎯 Total typing duration ~8 seconds

        for (int i = 0; i <= tip.Length; i++)
        {
            tipText.text = tip.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
