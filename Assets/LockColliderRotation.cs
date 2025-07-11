using UnityEngine;
public class LockColliderRotation : MonoBehaviour
{
    void LateUpdate()
    {
        // Force collider to stay upright
        GetComponent<CapsuleCollider>().transform.rotation = Quaternion.identity;
    }
}