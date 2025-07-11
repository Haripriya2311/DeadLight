using UnityEngine;
using TMPro;

public class BossAmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    void Update()
    {
        Gun activeGun = GetActiveGun();

        if (activeGun != null && ammoText != null)
        {
            ammoText.text = activeGun.currentAmmo + " / " + activeGun.reserveAmmo;
        }
        else if (ammoText != null)
        {
            ammoText.text = "-- / --";
        }
    }

    Gun GetActiveGun()
    {
        Gun[] allGuns = Object.FindObjectsByType<Gun>(FindObjectsSortMode.None);

        foreach (Gun gun in allGuns)
        {
            if (gun.gameObject.activeInHierarchy)
            {
                return gun;
            }
        }

        return null;
    }
}
