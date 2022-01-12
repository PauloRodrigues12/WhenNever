using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeaponUI : MonoBehaviour
{
    private SelectWeapon selectWeapon;

    //Armas
    public GameObject[] weapons;

    public int lastWeapon;

    private void Start()
    {
        selectWeapon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SelectWeapon>();

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[selectWeapon.currentWeapon].SetActive(true);
        lastWeapon = selectWeapon.currentWeapon;
    }
}
