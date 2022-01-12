using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    #region Variaveis

    //Ponto de disparo
    public Transform[] firePoints;
    public GameObject bulletPrefab;

    //Sprite da arma
    public SpriteRenderer spriteRenderer;

    //Joystick de disparo
    public FireJoystick fireJoystick;

    //Propriedades da arma
    public float fireRate;

    private bool allowFire;

    //Armas
    public bool isRifle;
    public bool isMinigun;
    public bool isSniper;
    public bool isShotgun;
    public bool isRevolver;
    public bool isRocket;

    //Audio
    private AudioSource audioSource;

    #endregion

    #region Start e Update

    private void Start()
    {
        StartCoroutine(Shoot());

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Rotação da arma
        if (fireJoystick.isShooting == true)
        {
            Vector2 lookDir = fireJoystick.displacement;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x);
            transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
        else
        {
            float angle = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        //Flip do sprite
        if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270)
            spriteRenderer.flipY = Physics2D.gravity.y < 0;
        else
            spriteRenderer.flipY = Physics2D.gravity.y > 0;

        //Disparar
        if (fireJoystick.isShooting == true)
            allowFire = true;
        else
            allowFire = false;
    }

    #endregion

    #region IEnumerators

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (allowFire == true)
            {
                //Instanciar bala

                //Rifle , Revolver, Sniper, Rocket
                if(isRifle == true || isRevolver == true || isSniper == true || isRocket == true)
                    Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);

                //Minigun
                if (isMinigun == true)
                {
                    int randomPos = Random.Range(0, firePoints.Length);
                    Instantiate(bulletPrefab, firePoints[randomPos].position, firePoints[randomPos].rotation);        
                }

                //Shotgun
                if(isShotgun == true)
                {
                    for (int i = 0; i < firePoints.Length; i++)
                    {
                        Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                    }
                }

                //Audio
                audioSource.Play();
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    #endregion
}
