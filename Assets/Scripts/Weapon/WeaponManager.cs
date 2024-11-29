using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponHandler[] weapons;

    private int currentWeaponIndex;

    private void Start()
    {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    private void Update()
    {
        ChangeWeapons();
    }

    void ChangeWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeaponIndex(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeaponIndex(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeaponIndex(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectWeaponIndex(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectWeaponIndex(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectWeaponIndex(5);
        }
    }

    void SelectWeaponIndex(int weaponIndex)
    {
        if (currentWeaponIndex == weaponIndex) return;

        weapons[currentWeaponIndex].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        currentWeaponIndex = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[currentWeaponIndex];
    }
}
