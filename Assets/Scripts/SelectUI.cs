using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectUI : MonoBehaviour
{
    //Scripts

    private LevelLoader levelLoader;
    private SelectWeapon selectWeapon;
    public RandomWeaponUI randomWeaponUI1;
    public RandomWeaponUI randomWeaponUI2;
    public RandomWeaponUI randomWeaponUI3;
    public CurrentWeaponUI currentWeaponUI;

    private void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        selectWeapon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SelectWeapon>();
    }

    //Começar transição de cena
    public void Changescreen()
    {
        levelLoader.wasFromTP = false;
        levelLoader.LoadNextScene();
    }

    //Selecionar arma
    public void SelectFrame1()
    {
        selectWeapon.currentWeapon = randomWeaponUI1.randomNr;
        Debug.Log("Frame 1");
    }
    public void SelectFrame2()
    {
        selectWeapon.currentWeapon = randomWeaponUI2.randomNr;
        Debug.Log("Frame 2");
    }
    public void SelectFrame3()
    {
        selectWeapon.currentWeapon = randomWeaponUI3.randomNr;
        Debug.Log("Frame 3");
    }
    public void SelectSameWeapon()
    {
        selectWeapon.currentWeapon = currentWeaponUI.lastWeapon;
        Debug.Log("Mesma arma");
    }
}
