using UnityEngine;

public class WeaponADS : MonoBehaviour
{
    public Transform adsPosition;           // Empty GameObject where the weapon should move while aiming
    public Transform hipPosition;           // Empty GameObject where the weapon stays normally
    public float aimSpeed = 10f;            // How fast to aim

    public Camera fpsCam;
    public float normalFOV = 60f;
    public float adsFOV = 40f;
    public float fovSpeed = 8f;

    private bool isAiming = false;

    void Update()
    {
        // 🔫 Right Mouse Button to aim
        isAiming = Input.GetButton("Fire2");

        if (isAiming)
        {
            // Move weapon to ADS position
            transform.position = Vector3.Lerp(transform.position, adsPosition.position, Time.deltaTime * aimSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, adsPosition.rotation, Time.deltaTime * aimSpeed);

            // Narrow camera FOV
            if (fpsCam != null)
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, adsFOV, Time.deltaTime * fovSpeed);
        }
        else
        {
            // Move weapon back to hip
            transform.position = Vector3.Lerp(transform.position, hipPosition.position, Time.deltaTime * aimSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, hipPosition.rotation, Time.deltaTime * aimSpeed);

            // Reset FOV
            if (fpsCam != null)
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, normalFOV, Time.deltaTime * fovSpeed);
        }
    }
}
