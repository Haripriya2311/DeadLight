using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    public Transform enemy; // Reference to the enemy's head or top
    public Vector3 offset = new Vector3(0, 2f, 0); // Adjust height
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (enemy != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(enemy.position + offset);
            transform.position = screenPos;
        }
    }
}
