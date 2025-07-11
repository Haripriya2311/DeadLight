using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    void Update()
    {
        Gun activeGun = GetActiveGun();

        if (activeGun != null && ammoText != null)
        {
            ammoText.text = activeGun.currentAmmo + " / " + activeGun.reserveAmmo;
            Debug.Log("Ammo Updated: " + ammoText.text);
        }
        else
        {
            Debug.LogWarning("Ammo not updated. activeGun or ammoText is null.");
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