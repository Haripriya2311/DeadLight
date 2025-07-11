using UnityEngine;

public class WeaponADSAndSway : MonoBehaviour
{
    [Header("ADS Settings")]
    public Vector3 adsPosition = new Vector3(0f, -0.1f, 0.3f);
    public float adsSpeed = 8f;

    [Header("Sway Position")]
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmooth = 6f;

    [Header("Sway Rotation")]
    public float swayRotationAmount = 4f;
    public float maxSwayRotation = 5f;
    public float swayRotationSmooth = 12f;

    [Space]
    public bool rotationX = true, rotationY = true, rotationZ = true;

    private Vector3 hipPosition;
    private Vector3 currentADSPos;
    private Quaternion initialRotation;

    private float inputX, inputY;

    void Start()
    {
        hipPosition = transform.localPosition;
        currentADSPos = hipPosition;
        initialRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        bool isAiming = Input.GetMouseButton(1);

        // Smooth ADS position
        Vector3 targetADS = isAiming ? adsPosition : hipPosition;
        currentADSPos = Vector3.Lerp(currentADSPos, targetADS, Time.deltaTime * adsSpeed);

        // Sway input
        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        // Sway position offset
        float swayX = Mathf.Clamp(inputX * swayAmount, -maxSwayAmount, maxSwayAmount);
        float swayY = Mathf.Clamp(inputY * swayAmount, -maxSwayAmount, maxSwayAmount);
        Vector3 swayOffset = new Vector3(swayX, swayY, 0f);

        // Apply final position
        transform.localPosition = Vector3.Lerp(transform.localPosition, currentADSPos + swayOffset, Time.deltaTime * swaySmooth);

        // Sway rotation
        float tiltY = Mathf.Clamp(inputX * swayRotationAmount, -maxSwayRotation, maxSwayRotation);
        float tiltX = Mathf.Clamp(inputY * swayRotationAmount, -maxSwayRotation, maxSwayRotation);

        Quaternion swayRotation = Quaternion.Euler(
            new Vector3(rotationX ? -tiltX : 0f,
                        rotationY ? tiltY : 0f,
                        rotationZ ? tiltY : 0f));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, swayRotation * initialRotation, Time.deltaTime * swayRotationSmooth);
    }
}