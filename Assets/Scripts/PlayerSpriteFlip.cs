using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteFlip : MonoBehaviour
{
    #region Variaveis

    private PlayerController playerController;
    private GameObject gun;
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Start e Update

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Arma selecionada
        if (playerController.isRifle == true)
            gun = playerController.guns[0].gameObject;
        if (playerController.isMinigun== true)
            gun = playerController.guns[1].gameObject;
        if (playerController.isSniper == true)
            gun = playerController.guns[2].gameObject;
        if (playerController.isShotgun == true)
            gun = playerController.guns[3].gameObject;
        if (playerController.isRevolver == true)
            gun = playerController.guns[4].gameObject;
        if (playerController.isRocket == true)
            gun = playerController.guns[5].gameObject;
    }

    private void Update()
    {
        //Flip do sprite do player
        if (gun.transform.eulerAngles.z >= 90 && gun.transform.eulerAngles.z <= 270)
            spriteRenderer.flipX = Physics2D.gravity.y < 0;
        else
            spriteRenderer.flipX = Physics2D.gravity.y > 0;
    }

    #endregion
}
