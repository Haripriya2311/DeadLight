using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cam;

    void LateUpdate()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
