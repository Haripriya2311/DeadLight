using UnityEngine;
using UnityEngine.UI;

public class GunScope : MonoBehaviour
{
    public Transform normalPos;     // 📌 Assign in Inspector
    public Transform scopedPos;     // 🎯 Assign in Inspector
    public float aimSpeed = 10f;

    public Camera fpsCam;
    public float normalFOV = 60f;
    public float scopedFOV = 30f;

    public Image redDotUI;          // 🔴 Assign the UI Image (red dot)

    private bool isAiming = false;

    void Update()
    {
        // 👉 Detect Right-Click
        isAiming = Input.GetButton("Fire2");

        // 🔁 Smooth transition between positions
        Transform target = isAiming ? scopedPos : normalPos;
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * aimSpeed);

        // 🔭 Adjust Camera FOV
        fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, isAiming ? scopedFOV : normalFOV, Time.deltaTime * aimSpeed);

        // 🔴 Show/hide red dot
        if (redDotUI != null)
        {
            Color c = redDotUI.color;
            c.a = isAiming ? 1f : 0f;
            redDotUI.color = c;
        }
    }
}
