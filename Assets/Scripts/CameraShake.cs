using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.05f;

    private Vector3 originalPos;
    private float currentShakeTime = 0f;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeTime > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;
            currentShakeTime -= Time.deltaTime;
        }
        else
        {
            currentShakeTime = 0f;
            transform.localPosition = originalPos;
        }
    }

    public void Shake()
    {
        currentShakeTime = shakeDuration;
    }
}