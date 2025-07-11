using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public float impactForce = 30f;
    public float reloadTime = 2f;

    [Header("Ammo")]
    public int maxAmmo = 30;
    public int currentAmmo;
    public int reserveAmmo = 90;

    [Header("References")]
    public Camera fpsCam;
    public CameraRecoil Recoil_Script;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadingText;
    public TextMeshProUGUI outOfAmmoText;
    public Gun otherGun;

    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    private AudioSource audioSource;
    private CameraShake cameraShake;

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        cameraShake = fpsCam.GetComponent<CameraShake>();
        Transform recoilTransform = transform.Find("CameraRot/CameraRecoil");
        if (recoilTransform != null)
            Recoil_Script = recoilTransform.GetComponent<CameraRecoil>();

        if (outOfAmmoText != null)
            outOfAmmoText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 && reserveAmmo > 0)
        {
            isReloading = true;
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmo <= 0 && reserveAmmo <= 0)
        {
            if (outOfAmmoText != null && !outOfAmmoText.gameObject.activeSelf)
                StartCoroutine(BlinkOutOfAmmo());

            if (otherGun != null &&
                otherGun.currentAmmo <= 0 &&
                otherGun.reserveAmmo <= 0 &&
                ScoreManager.Instance != null &&
                ScoreManager.Instance.enemiesRemaining > 0)
            {
                MissionFailedManager.Instance.TriggerMissionFailed("Out of ammo but zombies remain");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && reserveAmmo > 0)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        if (reloadingText != null)
            StartCoroutine(BlinkReloading());

        if (reloadSound != null)
            audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        int bulletsToLoad = maxAmmo - currentAmmo;
        if (reserveAmmo >= bulletsToLoad)
        {
            currentAmmo = maxAmmo;
            reserveAmmo -= bulletsToLoad;
        }
        else
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
        }

        if (reloadingText != null)
            reloadingText.gameObject.SetActive(false);

        isReloading = false;
    }

    IEnumerator BlinkReloading()
    {
        reloadingText.gameObject.SetActive(true);
        for (int i = 0; i < 6; i++)
        {
            reloadingText.enabled = false;
            yield return new WaitForSeconds(0.2f);
            reloadingText.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator BlinkOutOfAmmo()
    {
        outOfAmmoText.gameObject.SetActive(true);
        outOfAmmoText.color = Color.red;

        for (int i = 0; i < 6; i++)
        {
            outOfAmmoText.enabled = false;
            yield return new WaitForSeconds(0.2f);
            outOfAmmoText.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        outOfAmmoText.gameObject.SetActive(false);
    }

    void Shoot()
    {
        if (currentAmmo <= 0) return;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (fireSound != null && audioSource != null)
            audioSource.PlayOneShot(fireSound);

        if (cameraShake != null)
            cameraShake.Shake();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit " + hit.transform.name);

            float damageToApply = damage;

            if (hit.collider.CompareTag("EnemyHead"))
            {
                damageToApply *= 2f;
            }

            EnemyHealth enemy = hit.transform.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damageToApply);

            // ✅ Updated BossZombieHealth access using GetComponentInParent
            BossZombieHealth boss = hit.transform.GetComponentInParent<BossZombieHealth>();
            if (boss != null)
            {
                boss.TakeDamage(damageToApply);
                Debug.Log("✅ Hit Boss Zombie for " + damageToApply);
            }

            ExplosiveBarrel barrel = hit.transform.GetComponent<ExplosiveBarrel>();
            if (barrel != null)
                barrel.TakeHit();

            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        if (Recoil_Script != null)
            Recoil_Script.RecoilFire();

        GunKickback kickback = GetComponent<GunKickback>();
        if (kickback != null)
            kickback.ApplyKickback();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = currentAmmo + " / " + reserveAmmo;
    }
}
