using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    public int currentWeapon;

    /*
      isRifle = 0;
      isMinigun = 1;
      isSniper = 2;
      isShotgun = 3;
      isRevolver = 4;
      isRocket = 5;   
     */

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
