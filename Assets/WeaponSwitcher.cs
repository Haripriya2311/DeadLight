using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons; // Drag your weapons here in Inspector
    private int currentWeaponIndex = 0;

    void Start()
    {
        SwitchWeapon(0); // Start with first weapon
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0); // Rifle

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(1); // Pistol
    }

    void SwitchWeapon(int index)
    {
        if (index == currentWeaponIndex) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }

        currentWeaponIndex = index;
    }
}
