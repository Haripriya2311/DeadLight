using UnityEngine;
using UnityEngine.UI;

public class Scope : MonoBehaviour
{
    public Camera fpsCam;
    public Image scopeOverlay;
    public float scopedFOV = 20f;
    private float normalFOV;

    void Start()
    {
        normalFOV = fpsCam.fieldOfView;
        scopeOverlay.enabled = false;
    }

    void Update()
    {
        if (Input.GetButton("Fire2")) // Right mouse button held
        {
            scopeOverlay.enabled = true;
            fpsCam.fieldOfView = scopedFOV;
        }
        else
        {
            scopeOverlay.enabled = false;
            fpsCam.fieldOfView = normalFOV;
        }
    }
}
