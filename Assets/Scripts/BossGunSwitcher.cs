using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossGunSwitcher : MonoBehaviour
{
    public GameObject rifle;
    public GameObject pistol;

    public TextMeshProUGUI ammoText;
    public Button rifleButton;
    public Button pistolButton;

    private Gun currentGun;

    void Start()
    {
        if (rifleButton != null)
            rifleButton.onClick.AddListener(EquipRifle);

        if (pistolButton != null)
            pistolButton.onClick.AddListener(EquipPistol);

        EquipRifle(); // ✅ Start with rifle instead of pistol
    }

    void Update()
    {
        if (currentGun != null && ammoText != null)
        {
            ammoText.text = $"{currentGun.currentAmmo} / {currentGun.reserveAmmo}";
        }
    }

    void EquipRifle()
    {
        if (rifle != null && pistol != null)
        {
            rifle.SetActive(true);
            pistol.SetActive(false);
            currentGun = rifle.GetComponent<Gun>();
        }
    }

    void EquipPistol()
    {
        if (rifle != null && pistol != null)
        {
            rifle.SetActive(false);
            pistol.SetActive(true);
            currentGun = pistol.GetComponent<Gun>();
        }
    }
}
