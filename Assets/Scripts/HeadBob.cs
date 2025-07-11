using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Bob Settings")]
    public float bobSpeed = 8f;
    public float bobAmount = 0.05f;

    private float timer = 0f;
    private Vector3 startPos;
    private CharacterController controller;

    void Start()
    {
        startPos = transform.localPosition;
        controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if (controller == null) return;

        // If moving and on the ground
        if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            timer += Time.deltaTime * bobSpeed;
            float bobX = Mathf.Cos(timer) * bobAmount;
            float bobY = Mathf.Abs(Mathf.Sin(timer)) * bobAmount;
            transform.localPosition = startPos + new Vector3(bobX, bobY, 0);
        }
        else
        {
            // Smoothly return to starting position
            timer = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * bobSpeed);
        }
    }
}