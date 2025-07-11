using UnityEngine;

public class GunKickback : MonoBehaviour
{
    public Vector3 kickbackOffset = new Vector3(0, 0, -0.1f);
    public float kickbackSpeed = 10f;
    public float returnSpeed = 20f;

    private Vector3 originalPosition;
    private Vector3 currentOffset;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void ApplyKickback()
    {
        currentOffset += kickbackOffset;
    }

    void Update()
    {
        // Smoothly move towards target offset
        currentOffset = Vector3.Lerp(currentOffset, Vector3.zero, returnSpeed * Time.deltaTime);

        transform.localPosition = originalPosition + currentOffset;
    }
}