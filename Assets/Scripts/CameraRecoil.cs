using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [Header("Hipfire Recoil")]
    [SerializeField] private float recoilX = 2f;
    [SerializeField] private float recoilY = 1f;
    [SerializeField] private float recoilZ = 0.5f;

    [SerializeField] private float snappiness = 10f;
    [SerializeField] private float returnSpeed = 5f;

    public Vector3 CurrentRotation => currentRotation;


    void Update()
    {
        // Smoothly return recoil to zero over time
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        // Apply random horizontal and roll recoil
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));




    }
}