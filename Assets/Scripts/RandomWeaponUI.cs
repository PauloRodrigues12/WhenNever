using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeaponUI : MonoBehaviour
{
    //Armas
    public GameObject[] weapons;

    public int randomNr;

    private void Awake()
    {
        randomNr = Random.Range(0, 5);
    }

    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[randomNr].SetActive(true);
    }
}
