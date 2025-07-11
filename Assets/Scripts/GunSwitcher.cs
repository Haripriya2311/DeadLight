using UnityEngine;
using UnityEngine.UI;
using TMPro; // ✅ Required for TextMeshPro

public class GunSwitcher : MonoBehaviour
{
    public GameObject rifle;
    public GameObject pistol;

    public TextMeshProUGUI rifleAmmoText;
    public TextMeshProUGUI pistolAmmoText;

    public Button rifleButton;
    public Button pistolButton;

    private Gun currentGun;

    void Start()
    {
        rifleButton.onClick.AddListener(EquipRifle);
        pistolButton.onClick.AddListener(EquipPistol);
        EquipRifle(); // Default weapon
    }

    void Update()
    {
        if (currentGun != null)
        {
            // ✅ Show ammo only under the active weapon
            rifleAmmoText.text = rifle.activeSelf ? $"{currentGun.currentAmmo} / {currentGun.reserveAmmo}" : "";
            pistolAmmoText.text = pistol.activeSelf ? $"{currentGun.currentAmmo} / {currentGun.reserveAmmo}" : "";
        }
    }

    void EquipRifle()
    {
        rifle.SetActive(true);
        pistol.SetActive(false);
        currentGun = rifle.GetComponent<Gun>();
    }

    void EquipPistol()
    {
        rifle.SetActive(false);
        pistol.SetActive(true);
        currentGun = pistol.GetComponent<Gun>();
    }
}
