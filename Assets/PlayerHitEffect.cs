using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHitEffect : MonoBehaviour
{
    public Image bloodImage;
    public float showDuration = 1f;

    private Coroutine bloodCoroutine;

    public void ShowBloodEffect()
    {
        if (bloodCoroutine != null)
            StopCoroutine(bloodCoroutine);

        bloodCoroutine = StartCoroutine(ShowBloodRoutine());
    }

    IEnumerator ShowBloodRoutine()
    {
        bloodImage.color = new Color(1, 1, 1, 1); // Fully visible
        yield return new WaitForSeconds(showDuration);
        bloodImage.color = new Color(1, 1, 1, 0); // Invisible
    }
}
